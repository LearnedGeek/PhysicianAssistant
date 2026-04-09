# Azure Access Structure — Learned Geek

**Last Updated:** April 7, 2026

## Tenant

| Property | Value |
|---|---|
| Tenant Name | Learned Geek |
| Tenant ID | dda04e53-9603-4f72-8153-77e3270076ca |
| Primary Domain | learnedgeek.com |
| Default Domain | markmlearnedgeek.onmicrosoft.com |
| License | Microsoft Entra ID P2 (trial, 30 days) |

## Identity Providers

| Provider | Status |
|---|---|
| Microsoft Entra ID | Configured |
| Google | Configured (OAuth via Google Cloud project "Learned Geek Azure SSO") |
| Email one-time passcode | Configured |
| Microsoft (personal accounts) | Configured |

## Users

| User | UPN | Type | Role | Groups |
|---|---|---|---|---|
| Mark McArthey | markm@learnedgeek.com | Member | Global Administrator, Subscription Owner | Developers |
| Hannah Kraemer | hkraemer@learnedgeek.com | (create when she starts — June 2026) | User | Interns |

## Security Groups

| Group | ID | Purpose | Azure RBAC |
|---|---|---|---|
| Developers | 827da7a0-b57a-4c7a-8c08-ce12a2ce5486 | Full dev team — full access to dev/staging resources | Owner on dev/staging resource groups |
| Interns | 2e47d601-ed96-4b47-a499-d28bb6e9a716 | Temporary intern access — scoped to dev only | Contributor on dev resource group only |

## Resource Group Access Plan

| Resource Group | Developers | Interns | Notes |
|---|---|---|---|
| rg-drok-dev (future) | Owner | Contributor | Hannah's primary workspace |
| rg-drok-staging (future) | Owner | Reader | Can view but not modify staging |
| rg-drok-prod (future) | Owner | No access | Interns never touch production |
| rg-learnedgeek-platform | Owner | No access | TXT-GEEK POC — not intern scope |
| ani-is-an-idiot | Owner | No access | ANI project — not intern scope |

## RBAC Role Definitions

| Role | What It Allows |
|---|---|
| Owner | Full access — create, modify, delete resources + manage access |
| Contributor | Create, modify, delete resources — but cannot manage access (can't add/remove users) |
| Reader | View only — can see resources and their config but cannot change anything |

## When Hannah Starts (June 2026 Checklist)

1. Create Google Workspace account: hkraemer@learnedgeek.com
2. Create Entra ID user (or invite as guest via Google federation)
3. Add to "Interns" security group
4. Assign P2 license (if needed for SSO)
5. Create rg-drok-dev resource group
6. Assign Interns group Contributor role on rg-drok-dev
7. Add to GitHub repository with appropriate permissions
8. Share dev environment setup instructions
9. First-day onboarding: architecture walkthrough, tools access verification
