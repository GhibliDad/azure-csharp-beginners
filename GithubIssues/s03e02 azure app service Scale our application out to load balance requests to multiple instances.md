When the load on an application increases, one single instance running the application might not have enough performance capacity to handle the load. A well architected application should be able to scale (i.e. run it on more than one instance/machine). Let's scale our application to 2 instances and see how it's done.

### Goal
- Scaling
- Scale up vs Scale out
- Scalable applications

### Steps
1. Go to your `App Service` in the Azure portal.
2. Click on `Scale up (App Service Plan)`
3. Scale to the `B1` plan and wait until the operation is finished
    - It's pay-as-you-go for these Azure resources, don't go above `B1` and remember to scale down or remove the resources whenever possible (e.g. end of day).
4. Click on `Scale out (App Service Plan)`
5. Manually scale it to 2 instances
6. Call the api and verify that everything still works
7. Write another `Console` application that continuously calls the API while performing scaling operations. Is there any downtime during scaling operations?
8. Is it possible for your console application to see a difference in requests/seconds and request latency when running with different scaling settings in the App Service?
9. How many req/s can you achieve?

### Example output
```console
Response: OK - Call: 4984 - latency: 90 ms - rate/s: 393,1762649902001
Response: OK - Call: 4985 - latency: 91 ms - rate/s: 393,1947694983807
Response: OK - Call: 4986 - latency: 90 ms - rate/s: 393,2727362042906
Response: OK - Call: 4987 - latency: 90 ms - rate/s: 393,33432480609963
Response: OK - Call: 4991 - latency: 81 ms - rate/s: 393,6257580746279
Response: OK - Call: 4960 - latency: 162 ms - rate/s: 390,63294026917555
Response: OK - Call: 4958 - latency: 171 ms - rate/s: 390,42324691201634
Response: OK - Call: 4962 - latency: 163 ms - rate/s: 390,73206226823316
Response: OK - Call: 4963 - latency: 171 ms - rate/s: 390,5622070965428
Response: OK - Call: 4961 - latency: 171 ms - rate/s: 390,40457789305157
Response: OK - Call: 4964 - latency: 170 ms - rate/s: 390,6119056683264
Response: OK - Call: 4967 - latency: 170 ms - rate/s: 390,5944940205346
Response: OK - Call: 4965 - latency: 171 ms - rate/s: 390,4305557291021
Response: OK - Call: 4968 - latency: 171 ms - rate/s: 390,60803731231385
Response: OK - Call: 4966 - latency: 173 ms - rate/s: 390,4158429522633
Response: OK - Call: 4988 - latency: 131 ms - rate/s: 391,93355176792795
Response: OK - Call: 4969 - latency: 178 ms - rate/s: 390,43981438228786
Response: OK - Call: 4989 - latency: 131 ms - rate/s: 392,003394754113
Response: OK - Call: 4990 - latency: 130 ms - rate/s: 392,0804587445084
Response: OK - Call: 4992 - latency: 130 ms - rate/s: 392,176808011682
Response: OK - Call: 4994 - latency: 133 ms - rate/s: 392,03454570108175
Response: OK - Call: 4993 - latency: 140 ms - rate/s: 391,743257259429
Response: OK - Call: 4996 - latency: 140 ms - rate/s: 391,92381823103614
Response: OK - Call: 4995 - latency: 141 ms - rate/s: 391,84426102384987
Response: OK - Call: 4997 - latency: 139 ms - rate/s: 391,9944457752365
Response: OK - Call: 4998 - latency: 133 ms - rate/s: 392,0232784960117
Response: OK - Call: 4999 - latency: 129 ms - rate/s: 391,9032920966245
```