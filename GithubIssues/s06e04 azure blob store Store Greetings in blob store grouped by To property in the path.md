In blob store it's also possible to organise blobs using a hierarchy similar (but not the same as) folders in a typical file system. Let's try to store our Greeting blobs in a pattern using `From` and `To` by adding the values of these properties to the `blob name`. 

Using this type of hierarchy in the blob name can make it possible get all Greetings from a specific user, all Greetings to a specific user etc without downloading each blob and inspecting the contents of the blob.

### Goal
* Azure Blob Store
* Blob name hierarchy using `/`

### Steps
1. Update the blob name to `{from}/{to}/{id}` in the `CreateAsync` method in `BlobGreetingRepository
2. Update the `GetAsync` methods in `BlobGreetingRepository` to use the new format for `blob name`
3. Update the `UpdateAsync` method in `BlobGreetingRepository` to use the new format for `blob name`
4. Delete existing blobs that are not stored in the new blob name format before testing

### Example output
There are greetings from these names:
![image](https://user-images.githubusercontent.com/2921523/152860502-2d43138d-5175-40c0-baa7-c101d69b59b7.png)

There are greetings from Keen to these names:
![image](https://user-images.githubusercontent.com/2921523/152860565-c2dfcd26-84da-4f95-8023-39e644d67976.png)

There are greetings from Keen to Anton:
![image](https://user-images.githubusercontent.com/2921523/152860598-a55a1c3f-2f4f-4468-9c1a-499e87859440.png)
