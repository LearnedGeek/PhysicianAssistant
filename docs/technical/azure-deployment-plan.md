# DrOk POC — Azure Deployment Plan
## From Local/ngrok to Cloud-Hosted

**Date:** March 28, 2026
**Status:** Ready to execute
**Prerequisite:** POC is validated locally (Twilio → ngrok → localhost:5000)

---

## Goal

Deploy the working POC to Azure so it runs in the cloud without ngrok or a local machine. Martin can text the number anytime and get a response. Demo-ready.

---

## Architecture — Current (Local)

```
Phone → Twilio → ngrok → localhost:5000 → Claude API + PubMed → TwiML → Twilio → Phone
```

## Architecture — Target (Azure)

```
Phone → Twilio → Azure App Service → Claude API + PubMed → TwiML → Twilio → Phone
```

Only one thing changes: ngrok + localhost is replaced by Azure App Service. Everything else stays the same.

---

## Azure Resources Needed

| Resource | Azure Service | SKU / Tier | Est. Monthly Cost |
|---|---|---|---|
| App hosting | **App Service** | B1 (Basic) | ~$13/month |
| Resource group | `rg-drok-poc` | — | Free |
| Application Insights (optional) | **Monitor** | Free tier | $0 |
| Custom domain + SSL (optional) | App Service managed cert | — | Free with App Service |

**Total: ~$13/month for POC.** The B1 tier gives you 1.75GB RAM, always-on, custom domain support, and SSL. More than enough for a .NET 10 minimal API handling SMS webhooks.

**Not needed for POC:**
- Azure SQL / PostgreSQL — conversations are file-based for now
- Azure Key Vault — use App Service Configuration for secrets
- Azure Blob Storage — no file uploads yet
- Azure Front Door / CDN — single endpoint, no scaling needed
- Container Registry / AKS — App Service direct deployment is simpler

---

## Deployment Steps

### Phase 1 — Azure Setup (~15 minutes)

| # | Task | Notes |
|---|---|---|
| 1 | Log into Azure Portal with `markm@learnedgeek.com` | Per Learned Geek infra plan |
| 2 | Create resource group `rg-drok-poc` | Region: East US (closest to Twilio US infrastructure) |
| 3 | Create App Service Plan `plan-drok-poc` (B1 Linux) | Linux is cheaper and .NET 10 runs fine on it |
| 4 | Create Web App `drok-poc` (or `physicianassistant-poc`) | Runtime: .NET 10, Linux |
| 5 | Note the default URL: `https://drok-poc.azurewebsites.net` | This becomes the Twilio webhook URL |

### Phase 2 — Configuration (~10 minutes)

| # | Task | Notes |
|---|---|---|
| 1 | Go to Web App → Configuration → Application Settings | |
| 2 | Add `Claude__ApiKey` = your Anthropic API key | Double underscore = nested config in .NET |
| 3 | Add `Claude__Model` = `claude-sonnet-4-6` | |
| 4 | Add `Claude__MaxTokens` = `1024` | |
| 5 | Add `Claude__TimeoutSeconds` = `30` | |
| 6 | Add `PubMed__ApiKey` = your NCBI API key | |
| 7 | Add `PubMed__Email` = `markm@learnedgeek.com` | |
| 8 | Add `PubMed__ToolName` = `infanzia-triage` | |
| 9 | Add `PubMed__MaxResults` = `3` | |
| 10 | Add `Conversation__StoragePath` = `/home/conversations` | Linux persistent storage path |
| 11 | Set `ASPNETCORE_ENVIRONMENT` = `Production` | App Settings handles secrets; no appsettings.Development.json needed |

### Phase 3 — Deploy Code (~10 minutes)

**Option A: GitHub Actions (recommended)**

Create `.github/workflows/deploy.yml`:

```yaml
name: Deploy to Azure
on:
  push:
    branches: [main]
    paths: ['src/**']

jobs:
  deploy:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v4
      - uses: actions/setup-dotnet@v4
        with:
          dotnet-version: '10.0.x'
      - run: dotnet publish src/PhysicianAssistant.csproj -c Release -o publish
      - uses: azure/webapps-deploy@v3
        with:
          app-name: 'drok-poc'
          publish-profile: ${{ secrets.AZURE_PUBLISH_PROFILE }}
          package: publish
```

Then in GitHub repo settings → Secrets → add `AZURE_PUBLISH_PROFILE` (download from Azure Portal → Web App → Get Publish Profile).

**Option B: Azure CLI (quick one-time deploy)**

```bash
cd src
dotnet publish -c Release -o publish
az webapp deploy --resource-group rg-drok-poc --name drok-poc --src-path publish.zip --type zip
```

**Option C: VS Code Azure Extension**

Right-click the project → Deploy to Web App → select `drok-poc`. Point and click.

### Phase 4 — Wire Up Twilio (~2 minutes)

| # | Task | Notes |
|---|---|---|
| 1 | Go to Twilio Console → Phone Number → Messaging Configuration | |
| 2 | Change webhook URL from `https://nonperverse-marg-rateable.ngrok-free.dev/sms` to `https://drok-poc.azurewebsites.net/sms` | |
| 3 | Method: HTTP POST | Same as before |
| 4 | Send a test SMS | Verify end-to-end |

### Phase 5 — Verify (~5 minutes)

| # | Task | Expected |
|---|---|---|
| 1 | Hit `https://drok-poc.azurewebsites.net/health` in browser | `{"status":"healthy","service":"PhysicianAssistant Triage POC"}` |
| 2 | Send SMS: "My child has a fever" | English triage response within 10 seconds |
| 3 | Send SMS: "Fiebre en mi bebé de 1 año" | Spanish triage response |
| 4 | Send second message with follow-up symptoms | PubMed context should appear in logs |
| 5 | Check Application Insights (if enabled) | Request logs, response times |

---

## Conversation Persistence on Azure

The current `FileConversationService` writes JSON files to disk. On Azure App Service:

- **`/home` is persistent** on Linux App Service — survives restarts and redeployments
- Set `Conversation__StoragePath` to `/home/conversations`
- Files persist as long as the App Service exists
- For production: migrate to Azure Blob Storage or PostgreSQL (but fine for POC)

---

## Custom Domain (Optional — Do Later)

When ready to add `api.learnedgeek.com`:

| # | Task | Notes |
|---|---|---|
| 1 | In Cloudflare DNS, add CNAME: `api` → `drok-poc.azurewebsites.net` | Proxy status: DNS only (gray cloud) initially |
| 2 | In Azure Portal → Web App → Custom Domains → Add `api.learnedgeek.com` | Azure validates the CNAME |
| 3 | Enable managed SSL certificate | Free with App Service |
| 4 | Update Twilio webhook to `https://api.learnedgeek.com/sms` | |
| 5 | In Cloudflare, switch proxy status to Proxied (orange cloud) if desired | Optional — adds Cloudflare CDN/WAF |

---

## Security Considerations

| Item | POC Status | Production Requirement |
|---|---|---|
| HTTPS | Enforced by default on Azure | ✅ |
| API keys in env vars (not code) | App Service Configuration | ✅ |
| No appsettings.Development.json deployed | .gitignore handles this | ✅ |
| Twilio request validation | Not implemented in POC | Add `ValidateRequest` middleware before production |
| Rate limiting | Not implemented | Add before production (prevent abuse via public webhook) |
| IP whitelisting | Not implemented | Optional — can restrict to Twilio IPs |
| Conversation data encryption at rest | Not implemented | Required for production (Ley 29733 compliance) |

---

## Relationship to Learned Geek Infrastructure Plan

From the Learned Geek infra plan:

| Infra Plan Item | DrOk Relevance |
|---|---|
| Azure resource group `rg-infanzia-dev` | Rename or create `rg-drok-poc` for clarity |
| Azure Static Web App (Blazor) | Future — physician dashboard (Phase 4). Not needed for POC. |
| GitHub Actions auto-deploy | Recommended deployment method (Option A above) |
| Intern resource group access | Intern gets `rg-drok-poc` access only — no subscription-level permissions |
| Azure CLI on workstation | Already planned for Phase 4 workstation build |
| Tailscale remote access | Not needed for Azure (cloud-hosted) but useful for local dev |

---

## Cost Summary

| Item | Monthly Cost |
|---|---|
| App Service B1 | ~$13 |
| Application Insights (free tier) | $0 |
| Custom domain + SSL | $0 |
| Conversation file storage | Included in App Service |
| **POC Total** | **~$13/month** |

**Production estimate (future):**

| Item | Monthly Cost |
|---|---|
| App Service B2/S1 | $25-50 |
| Azure PostgreSQL (Basic) | $25-35 |
| Azure Blob Storage | $1-5 |
| Application Insights (standard) | $5-10 |
| **Production Total** | **~$60-100/month** |

---

## Checklist — Ready to Deploy When:

- [x] POC validated locally (SMS → Twilio → Claude + PubMed → response)
- [x] Bilingual triage working (English + Spanish)
- [x] File-based conversation persistence working
- [x] appsettings.Development.json gitignored (secrets not in code)
- [ ] Azure account active under `markm@learnedgeek.com`
- [ ] `rg-drok-poc` resource group created
- [ ] App Service created and configured
- [ ] Secrets added to App Service Configuration
- [ ] Code deployed (GitHub Actions, CLI, or VS Code)
- [ ] Twilio webhook URL updated
- [ ] Health check verified
- [ ] End-to-end SMS test passed

---

*Prepared by Mark McArthey, Learned Geek LLC*
*Reference: Learned Geek Infrastructure Plan (E:\Documents\Work\dev\repos\LearnedGeek\docs\learned-geek-infrastructure-plan.md)*
