# .Net Cli Templates

<img src="https://avatars.githubusercontent.com/u/9841374?s=200&v=4" align="right" alt="UK Government logo">

[![Build Status](https://dev.azure.com/sfa-gov-uk/Digital%20Apprenticeship%20Service/_apis/build/status/_projectname_?branchName=master)](https://dev.azure.com/sfa-gov-uk/Digital%20Apprenticeship%20Service/_build/latest?definitionId=_projectid_&branchName=master)
[![Jira Project](https://img.shields.io/badge/Jira-Project-blue)](https://skillsfundingagency.atlassian.net/secure/RapidBoard.jspa?rapidView=564&projectKey=_projectKey_)
[![Confluence Project](https://img.shields.io/badge/Confluence-Project-blue)](https://skillsfundingagency.atlassian.net/wiki/spaces/_pageurl_)
[![License](https://img.shields.io/badge/license-MIT-lightgrey.svg?longCache=true&style=flat-square)](https://en.wikipedia.org/wiki/MIT_License)

This repository holds the `dotnet cli` templates for creating new DAS websites, API's and Azure Functions. The templates make the process of creation of standard DAS applications easier and more straight forward by including and configuring many of the common packages and patterns without the developer having to do so manually.

Each template should be a good starting point for most new solutions of their type and help you get up and running as quickly as possible but should be amended as is required.

## How It Works

The templates have been configured to use the current LTS versions of .Net Core and to include a lot of the common packages and patterns that we use within most of our new applications.

All of the templates have multiple projects configured in the [Onion Architecture](https://skillsfundingagency.github.io/das-technical-guidance/development_standards/solution-structure#solution-architecture) layout, an example would look like this but there may be more or less depending on the type of project being created:

* SFA.DAS.xxx.Web
* SFA.DAS.xxx.Web.UnitTests
* SFA.DAS.xxx.Application
* SFA.DAS.xxx.Domain
* SFA.DAS.xxx.Infrastructure
* SFA.DAS.xxx.Infrastructure.UnitTests

### das-web Template

das-web is currently templated as if it were being configured as a subsite of EAS. It contains the JavaScript and CSS from das-frntend and two example pages. An unsecured `Home` page and a `Secure` page secured using `DasAuthorization`.

For the secured page to operate correctly you will need to create the required CosmosDB instance which consists of a database called `SFA.DAS.EmployerAccounts.ReadStore.Database`  containing a collection called `AccountUsers`. Within the collection you will need at least one entry for your user that looks like the example below but with your own userRef and the accountId for the employer you wish to use:

> The userRef is the Guid of the user taken from the [EmployerUsers](https://github.com/SkillsFundingAgency/das-employerusers) database in the AT environment.

```json
{
    "userRef": "<your user ref>",
    "accountId": 1,
    "role": 1,
    "outboxData": [
        {
            "messageId": "d08e03ec-e72b-4a0c-ab7b-64a57d8fcdc6",
            "created": "2021-04-07T13:14:11.106231Z"
        }
    ],
    "Created": "2021-04-07T13:14:11.106231Z",
    "updated": null,
    "removed": null,
    "id": "60907f76-f212-4a2d-b7bd-9a015c3d1bb9",
    "metadata": {
        "schemaType": "accountUser",
        "schemaVersion": 1
    },
    "_rid": "hnZXALDVXNUBAAAAAAAAAA==",
    "_self": "dbs/hnZXAA==/colls/hnZXALDVXNU=/docs/hnZXALDVXNUBAAAAAAAAAA==/",
    "_etag": "\"00000000-0000-0000-6444-f80ac33301d7\"",
    "_attachments": "attachments/",
    "_ts": 1624022541
}
```

It currently contains the following packages:

|Internal |External|
--- | --- 
|DAS Authorization|Fluent Validation|
|DAS Shared UI|App Insights|
|DAS Employer URL Helper|REDIS HealthCheck|
|DAS Encoding|Data protection|
|DAS HTTP|NLog|
|DAS REDIS NLog|Azure Table Storage|
|DAS Validation|NUnit|
|DAS NServiceBus|Moq|
||Autofixture|

### das-api Template

The Api template is configured with the following packages:

|Internal |External|
--- | --- 
|DAS Authorization|Fluent Validation|
|DAS REDIS NLog|Mediatr|
|DAS Unit of Work|REDIS HealthCheck|
|DAS NServiceBus|Data protection|
||NLog|
||Swashbuckle|
||App Insights|
||Azure Table Storage|
||NUnit|
||Moq|
||Autofixture|
||FluentAssertions|

### das-func Template

das-func is currently configured with the following packages and is configured with an NServiceBus trigger function as an example and with the RestEase library for calling API's

|Internal |External|
--- | --- 
|DAS HTTP|NLog|
|DAS REDIS NLog|Azure Table Storage|
|DAS Validation|NUnit|
|DAS NServiceBus|Moq|
|DAS Configuration|Autofixture|
||RestEase|
||Data protection|
||App Insights|

## 🚀 Installation

You can either clone this repository and use the combination of `dotnet pack` & `dotnet new -i <PATH_TO_NUGET>` commands from the `working` directory or 
install the templates from the nuget package hosted on nuget.org using `dotnet new -i <NUGET_ID>`

Once the templates are installed they can be used with the following commands `dotnet new -i <das-web|das-api>`.

## Contributing

Projects created from the templates should compile and run straight away. If you find any issues or want to add anything to the template please create a PR after first ensuring that any changes you make leave the templates making projects that compile and run.

Please also remember to update this README file with the relevant information for any changes made.

## 🐛 Known Issues

There are no known issues