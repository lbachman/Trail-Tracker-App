# To add MySql to your systems path 
1. get the path to the MySql Server .exe file and paste it to the clipboard. 
2. search for edit environmental variables in the windows search bar. 
3. click edit environmental variables
4. click edit on the PATH variable in System variables. 
5. click new and add exe path. 

# Enter MySql in Powershell
1. first ensure that the environment variables are set up correctly
2. enter `mysql -u root -p` followed by the password

# Project Type 
- ASP.NET Core Web App (Razor Pages) - this gives you example pages to start from.

# Requirements 
- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.Design
- Microsoft.EntityFrameworkCore.tools
- MySql.EntityFrameworkCore

# Using a Powershell Script to install Packages
- create the script to install the nuget packages. 
- open the package manager console. 
- navigate to where the script is located. 
- execute the script by entering .\script-name.ps1

# Getting the dotnet ef commands to work in powershell
`dotnet tool install --global dotnet-ef`

# Scaffold Database 
 -`dotnet ef dbcontext scaffold "Server=127.0.0.1;Port=3306;User=root;Database=wildlifeconservation;Pwd=110494;" MySql.EntityFrameworkCore -o Models`

# To Add an Additional Razor Page
- Right Click Pages folder and click Add Razor Page
- add content to page
- update nav bar in Layout page

# Register DbContext 
```var wildlifeConnection = builder.Configuration.GetConnectionString("WildlifeConnection") ?? throw new InvalidOperationException("Connection string 'WildlifeConnection' not found.");
builder.Services.AddDbContext<WildlifeconservationContext>(options => options.UseMySQL(wildlifeConnection));
``` 

# Configure Connection String

``` 
"AllowedHosts": "*",
"ConnectionStrings": {
"WildlifeConnection": "Server=127.0.0.1;Port=3306;User=root;Database=wildlifeconservation;Pwd=110494;"
} 
 ```

# To add a scaffolded razor page. 
- ensure the database context is registered in the program.cs
- create subfolders for each model you want to scaffold in the pages directory
- click add new scaffolding item in each folder 
- Update layout to include links to scaffolded pages. 

# Setting Up Identity

## Requirements
- Microsoft.AspNetCore.Identity.EntityFrameworkCore
- if you scaffold the identity thing, more nuget packages will be automatically installed. 

*can use rebuild instead of restarting to get nuget packages installed.*

### Deploying the Identity part of the Database
1. create Data folder in project
2. create application DbContext Class
3. add user class
4. register db context for asp.net identity in the program.cs file. if you want all the identity stuff in the main schema use the same connection string.
3. Run migration `Add-Migration -context ApplicationDbContext db1`
6. Update the database `Update-Database -context ApplicationDbContext`

# Scaffolding the Identity Stuff
1. right click in a folder
2. click scaffold new item
3. click Identity
4. select whatever options you intend to implement
5. select the ApplicationDbContext as the context. 
6. Finally add `<partial name="_LoginPartial" />` just below the <ul> tag in the nav bar to show the login partial


# In Conclusion 
1. Run Nuget Powershell script
2. Rebuild Solution
3. configure connection string
4. Add ApplicationDbContext & Application User class
5. Register ApplicationDbContext in program.cs
6. Add migration and update database
7. Scaffold Identity Object
8. Add partial thing to nav bar
9. Run SQL script in database
10. Scaffold entities
11. REgister entity Context 
12. Make folders for each entity in pages folder
13. scaffold entity CRUD razor pages 
14. Update nav bar to link to scaffolded razor pages. 

## To Add Image Upload & Viewing...

- Images go in the wwwroot folder. you can make subfolders in this folder
- create a razor page to display the pictures. below is the implemintation: 
```
<div class="container">
    <div class="row">
        @foreach (var picture in Model.Pictures)
        {
            <div class="col-md-3">
                <div class="card mb-4 shadow-sm">
                    <img src="@picture.FilePath" class="card-img-top" alt="Picture" style="max-width: 100%; height: auto;">
                    <div class="card-body">
                        <p class="card-text">@picture.UploadedBy</p>
                        <p class="card-text">@picture.UploadDate</p>
                        <p class="card-text">@picture.FilePath</p>
                    </div>
                </div>
            </div>
        }
    </div>
</div>
```