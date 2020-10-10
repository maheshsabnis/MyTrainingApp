.NET Core 3.x Publish Features
- Single File Publish for Desktop Apps
	- Modify the .csproje (Project) file for the deployment
		<RuntimeIdentifier>win10-x64</RuntimeIdentifier>
	  <PublishSingleFile>true</PublishSingleFile>
		- Publish single file will publish the complete application and .NET Core Framework in single
			.exe file which can be used as XCopy deployment
	- Build the project in Release Mode and then Pubish
	-  <PublishTrimmed>true</PublishTrimmed> settings in .csproje will publish the app with only
	 required dependencies

	 https://docs.microsoft.com/en-us/dotnet/core/rid-catalog
==========================================================================================================

Database Queries

1. Create Database Company
2. Use Company
3. USE [Company]
GO

/****** Object:  Table [dbo].[Department]    Script Date: 10/5/2020 8:53:17 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Department](
	[DeptNo] [int] NOT NULL,
	[DeptName] [varchar](100) NOT NULL,
	[Location] [varchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[DeptNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

4. USE [Company]
GO

/****** Object:  Table [dbo].[Employee]    Script Date: 10/5/2020 8:54:25 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Employee](
	[EmpNo] [int] NOT NULL,
	[EmpName] [varchar](100) NOT NULL,
	[Designation] [varchar](100) NOT NULL,
	[Salary] [int] NOT NULL,
	[DeptNo] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[EmpNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Employee]  WITH CHECK ADD FOREIGN KEY([DeptNo])
REFERENCES [dbo].[Department] ([DeptNo])
GO





ASP.NET Core Programming
- Create Models using EntityFrameworkCore (EFCore)
	- Insall EFCore Packages
		- Microsoft.EntityFrameworkCore
		- Microsoft.EntityFrameworkCore.SqlServer
		- Microsoft.EntityFrameworkCore.Design
		- Microsoft.EntityFrameworkCore.Tools

- TO install 'dotnet ef' command in global scope run the followi gcommand

dotnet tool install --global dotnet-ef

	
	- Database First Approach
		- Database is ready with tables and Model classes are generated using following command from the
		Command Prompt
		- dotnet ef dbcontext scaffold "<Connection-String>" 
				Microsoft.EntityFrameworkCore.SqlServer -o Models -t <TableName1> <tablename2>..		

Creating Models using EF Core Database First Approach

-- If using Windows Auth.
dotnet ef dbcontext scaffold "Data Source=.;Initial Catalog=Company;Integrated Security=SSPI;MultipleActiveResultSets=true" Microsoft.EntiryFrameworkCore.SqlServer -o Models  

-- If using SQL Auth.
dotnet ef dbcontext scaffold "Data Source=.;Initial Catalog=Company;User Id=sa;Password=<PWD>;MultipleActiveResultSets=true" Microsoft.EntiryFrameworkCore.SqlServer -o Models  





		- The above command will generate DbContext class with CLR objects aka entity classes mapped
		with tables used in command using <TableName>
			
	- Code-First Approach
		- Create Entity Classes with COnstraints and Relationships
		- Create DbContext class that is having public DbSet<EntityClass> proprties
		- Define mapping / relationship explicitely using code
		- Generate Database and tables using following commands
		- Generate Migrations
			- dotnet ef migrations add <MIGRATIONNAME> -c <Namespace-Path-of-DbContext-Class>
				- Generate Migration and mapping files for the database
		- Update database (Create database if not exist and generate tables in it)
			- dotnet ef database update -c <Namespace-Path-of-DbContext-Class>
			
	- DbContext Object
		- Manage Db Connections
		- Map the CLR object or Entity Class with Database table
			- DbSet<T> class to map CLR object with the Database Table
				- T is the CLR object that will mapp with Table of name T
				- e.g. DbSet<Product>, Product class map with Product Table
		- Manage Transactios
	- Consider CompanyContext is class derived from DbContext and have DbSet<Employee> Employees property
		then
		- Consider 'ctx' is an instance of CompanyContext class
		- To read all data from Employees table
			- ctx.Employees.ToList(); ctx.Employees.ToListAsync();
			- returns an IEnumerable<Employee>
		- TO Create a new Employee
			- Create an instance of Employee class e.g emp
			- Set all Property Values
			- ctx.Employees.Add(emp); OR ctx.Employees.AddAsync(emp);
			- Commit Transactions
				- ctx.SaveChanges(); OR ctx.SaveChangesAsync();
		- To Read a record based on Primary Key
			- ctx.Employees.Find(P.K.); OR ctx.Employees.FindAsync(P.K.);
		- To update  record
			- Search record based on P.K.
			- Update its property values
			- COmmit Transactions
		- To Delete record based on P.K.
			- Search record based in P.K.
			- ctx.Employess.Remove(Searched Record);
			- Commit Transactions

			
- ASP.NET Core MVC Folders (with Individual User Authentication)
	- Models
		- Contains all Model classes e.g. Entity Classes, Data Access Logic, Validations, 
			Model Specific bsiness Logic
	- Controllers
		- MVC Controllers 
	- Views
		- MVC Views
		- Layout Views
		- Shared Views e.g. Error
	- wwwroot folder
		- This folder will be used to contains 'static files(?)'
			- Static files are
					- JavaScript Files
					- CSS Files
					- Images
		- This folder will be used by hosting process (Self-Host/IIS/Docker/Cloud) to read and add the
			static files in HTTP Response
	- Area
		- Folder to contains Identity Web Razor Pages (WebForm)
		- This folder will also contain the separate MVC project structure 

- Frameworks
	- Microsoft.NETCore.App
		- The .NET Core Process, used to provide the following
			- Process to execute .NET Core
			- Memory Management
			- Porvides Standard Libraries to Application
	- Microsoft.AspNetCore.App
		- Package for ASP.NET Core
			- Hiosting
			- Dependency Injections
			- Sessions
			- MVC Request Processing
			- Web Forms Request Processing
			- API Request Processing
			- Identity
			- Caching
			- SignalR
	
 

- ASP.NET Core MVC and API Apps
	- Create ASP.NET Core application and understand the Concept of Project
		- Program class containing Main() metod
			- The Host class that will create an instace of Hosting Environment using HostBuilder to
				- Create Host
				- Manage Server
				- Manage Dependencies using Startup class
					- Constructor Injects the Appliction Confguration in Startup class using
						- IConfiguration interface
						- Application COnfigurations
							- ConnectionString
								- Database Connections
								- CosmosDB Connnections
								- RedisCache Connections
							- Logging
							- Hosting
							- Custom Configuration
								- Any Custom Settings e.g. JWT Keys
					- Cookies
					- Dependency Container
						- Database Connections
						- Sessions
						- Identity Management
						- CORS Policies
					- Reuest Processing for
						- WebForms
						- MVC And API Controllers Together
						- API Controllers
				- Manages Middlewares using Startup class

		- Startup class
			- Uses IConfigration interface as constructo injected
			- Have 'ConfigureServices()' method accepting IServiceCollection as input parameter
				- IServiceCollection, interface impelemnts IList<ServiceDescrioptor>, 
						ICollection<ServiceDescripor>, IEmumerable<ServiceDescriptor>
				- ServiceDescriptor class
					- Used to look for dependency types (aka classes) and register in DI Container using
						following strategies (aka lifecycle management)
							- Singleton, always global
								- The type (instance of class) will be registered throught 
									the lifetime of the applciation
							- Scoped, always stateful
								- The type will be registered for a lifetime of the session 
							- Transient, always stateless
								- The type will be instantiated for a specific request
				- This method manages	
					- DI Container for
						- Registering DbContext objects
						- Custom Repositories
							- Repository Pattern having the Business classes registered in DI Container
						- Identity
							- Authentication
								- User Based Authentication
							- Authorization
								- Role Based Security
								- Policies
									- Combination of multiple roles
						- Cross-Origin-Resource-Sharing (CORS) policy for API
						- Session configuration
						- Caching Configuration
					- WebForm Request Processing
						- AddRazorPages() method
					- MVC COntrollers and API Controllers Processing
						- AddControllersWithViews() method
					- API COntroller Processing
						- AddControllers();
			- Have 'Configure()' method accepting IApplicationBuilder and IHostingEnvironment parameters
				- IApplicationBuilder
					- Used to manage Middlewares to Execute or Process Http Request
				- IHostingEnvironment 
					- USed to manage the Hosting environment to execute the application. 

	- Create Repositories
	- Create Controllers
		- Model Validations
		- Exception Management
		- Filers
	- Adding Views
		- ViewResult(), the Razor Page, bound to the model and will display data from model or accept data
			for model
		- PartialViewResult(), the Razor reusable page bound to the model and will display data from model or accept data
			for model
		- ASP.NET Core uses 'TagHelper(?)' top bind the Model classes with Views 
			- They are the Custom Attributes to HTML elements having server-side executions 
			- asp-for
				- used to bind model property with HTML element
			- asp-action
				- used to execute the Action from from control in PostBack or Routing
			- asp-controller
				- used to load the controller in postback or routing
			- asp-items
				-  Used to generate HTML elements based on Collection
			- asp-validation-for
			- asp-route
	- Sessions
	- Security
	- Create APIs
		- Security
		- HTTP Methods
		- Model Binders
	- Middlewares
- Deployment