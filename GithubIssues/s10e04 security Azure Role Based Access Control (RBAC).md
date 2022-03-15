This isn't an exercise per se but just an area that is good to read a little bit about.

To manage access to different Azure resources for developers (and other identities) we use something called RBAC. RBAC stands for Role Based Access Control and is part of Azure AD (AAD). Basically there are a bunch of built in roles in AAD that gives a set of permissions resources within a scope. A scope can be a whole subscription, a resource group, a specific resource, a management group or similar.

RBAC:
https://docs.microsoft.com/en-us/azure/role-based-access-control/overview

Read this material from the perspective of a user, you will probably not need to manage RBAC as an admin but you or your application resources will be provided access through RBAC.