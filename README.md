# ReactContactListAPI


## Initial Setup

SQL Server Database is used for this solution. The connection string setup points to a hosted database onna remote server. You can use that database without requiring to setup SQL Server on your local machine. Or you can update connection string to point to a local database then run "update-database" to execute EF Core migration.

In other words, you can just run this API and start consuming it, with no code/config changes.



## Controller

This is a standard WebAPI written in C# with a Contact Controller that exposes these actions, also available with OpenAPI in Development:

![image](https://user-images.githubusercontent.com/71719282/185737545-78e6f261-12ef-4dbd-a759-e38a03448e77.png)

The actions take these parameters and return as below:

Task<ActionResult<GlobalResponse<int>>> SaveContact(Contact record)

Task<ActionResult<GlobalResponse<bool>>> UpdateContact(Contact record)
  
Task<ActionResult<GlobalResponse<bool>>> DeleteContact(int id)
  
Task<ActionResult<Contact>> GetContact(int id)
 
Task<ActionResult<List<Contact>>> GetContacts()
  

  
## Response schema 

The response schema looks like below:
  
![image](https://user-images.githubusercontent.com/71719282/185737734-02767469-6cec-4826-b76b-9e7bdfba5d60.png)
