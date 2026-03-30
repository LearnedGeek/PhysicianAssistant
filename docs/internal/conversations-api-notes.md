
Name	Value
Admin__ApiKey	Pick any secret string — e.g., lg-admin-2026 or generate something random

Then you can view conversations at:

https://learnedgeek-platform-bngjg7e0e9fah2fp.centralus-01.azurewebsites.net/admin/conversations?key=lg-admin-2026

Deploying now. Add the Admin__ApiKey environment variable in Azure while it deploys. Pick any secret value you'll remember.

Once deployed (~30 seconds), you'll have:

Dynamic blog content — RSS feed refreshes every 6 hours, new posts show up automatically
Admin visibility — hit /admin/conversations?key=YOUR_KEY in a browser to see who's texting and what they're asking
AI disclosure — first message identifies itself as an AI assistant
Text TXT-GEEK again to test the full flow.