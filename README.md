# GiftProject

## Framework  and Language version
-	Dot Net 5.0

## Technologies
    Front end and Back end
-   HTML
-   CSS
-	Asp Net Core MVC
## Author

- [Nesho Neshev]( https://github.com/NeshoNeshev)

## Project Overview

-	Project dependency

![Dependencies Graph](https://github.com/NeshoNeshev/GiftProject/blob/master/GiftProjectDiagram.jpg)

-	Project structure

![image](https://github.com/NeshoNeshev/GiftProject/blob/master/GiftProject.jpg)

### Common

** GiftProject.Common** contains global things. For example:

- [GlobalConstants.cs](https://github.com/NeshoNeshev/GiftProject/blob/master/GiftProject.Common/GlobalConstants.cs).

### Data

This solution folder contains two subfolders:

- GiftProject.Data.Data
- GiftProject.Data.Common
- GiftProject.Data.Models

#### GiftProject.Data

[GiftProject.Data](https://github.com/NeshoNeshev/GiftProject/tree/master/Data/GiftProject.Data) contains DbContext, Migrations and Configuraitons for the EF Core and Seeding users and roles.

Roles

- Administrator
Opportunities
- Create category
- Create product
- All categories
- All products
- Search by Catalogue number 
- Search by name
#### GiftProject.Data.Common

[GiftProject.Data.Common](https://github.com/NeshoNeshev/GiftProject/tree/master/Data/GiftProject.Data.Common) contains Repository and Base models.

#### GiftProject.Data.Models

[GiftProject.Data.Models](https://github.com/NeshoNeshev/GiftProject/tree/master/Data/GiftProject.Data.Models) contains backend models, Deletable Models, Enumerations, ApplicationUser and ApplicationRole classes and interfaces.


### Services

This solution folder contains three subfolders:
- GiftProject.Services
- GiftProject.Services.Data
- GiftProject.Services.Mapping
- GiftProject.Services.Messaging

#### GiftProject.Services.Data

[GiftProject.Services.Data](https://github.com/NeshoNeshev/GiftProject/tree/master/Services/GiftProject.Services.Data) wil contains service layer and logic.
- GiftProject.Services.Data contains many services, interfaces and one subfolder:

- Services
- - CatalogueNumber Service
- - Category Service
- - Cloudinary Service
- - Product Service 
- - Rating Service

- Interfaces
- - ICatalogueNumber Service
- - ICategory Service
- - ICloudinary Service
- - IProduct Service
- - IRating Service
- - IVote Service


#### GiftProject.Services.Mapping

[GiftProject.Services.Mapping](https://github.com/NeshoNeshev/GiftProject/tree/master/Services/GiftProject.Services.Mapping) provides simplified functionlity for auto mapping.

#### GiftProject.Services.Messaging

[GiftProject.Services.Messaging](https://github.com/NeshoNeshev/GiftProject/tree/master/Services/GiftProject.Services.Messaging) a ready to use integration with [SendGrid](https://sendgrid.com/).
### Tests

This solution folder contains three subfolders:

- GiftProject.Services.Data.Tests
- GiftProject.Web.Tests

### Web

This solution folder contains three subfolders:

- GiftProject.Web
- GiftProject.Web.ViewModels
- GiftProject.Web.Infrastructure

#### GiftProject.Web

[GiftProject.Web](https://github.com/NeshoNeshev/GiftProject/tree/master/Web/GiftProject.Web) contains Areas, Controllers and Views.

#### GiftProject.Web.ViewModels
[GiftProject.Web.ViewModels](https://github.com/NeshoNeshev/GiftProject/tree/master/Web/GiftProject.Web.ViewModels) contains View Models.

#### GiftProject.Web.Infrastructure

[GiftProject.Web.Infrastructure](https://github.com/NeshoNeshev/GiftProject/tree/master/Web/GiftProject.Web.Infrastructure) contains GoogleRecaptchaValidation and Pagination.

## Support


If you are having problems, please let me know by [raising a new issue](https://github.com/NeshoNeshev/GiftProject/issues).


## License

This project is licensed with the [MIT](https://https://github.com/NeshoNeshev/GiftProject/blob/master/LICENSE.MD).

