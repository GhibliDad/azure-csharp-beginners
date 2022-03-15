Let's add execution of our unit tests in the CI/CD pipeline. This adds another check to help us avoid deploying faulty code. We only want to deploy if all tests pass. This adds a level of regression testing every time we push our code.

### Goal
- Infrastructure as Code (IaC)
- CI/CD
- Bicep
- DevOps
- Github Actions
- YML

### Steps
1. Try looking at this document and see if you can figure out how to trigger unit tests in the workflow:
    - https://docs.microsoft.com/en-us/dotnet/devops/dotnet-test-github-action
2. Push to `main` and verify that the tests are executed
3. Try updating a test to make it always fail and push to `main`. What happens if a test fails?

### Example output
<img width="1104" alt="image" src="https://user-images.githubusercontent.com/2921523/151557330-79804f9a-a706-466b-a03d-ff52ce4e0f88.png">

<img width="1193" alt="image" src="https://user-images.githubusercontent.com/2921523/151558258-f1881e95-13b3-4d44-89d1-20ed7da5aaf9.png">
