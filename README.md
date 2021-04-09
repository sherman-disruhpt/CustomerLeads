# Overview 

## Step 1 - Build a system to parse and sort a set of records  

Create a command line app that takes as input a file with a set of leads in one of three formats described below, and outputs (to screen) the set of leads sorted in one of three ways. 

### Input  

A lead consists of the following 6 fields: last name, first name, property type (house, condo, trailer),  start date, project, and phone number. The input is 3 files, each containing records stored in a different format.  

• The pipe-delimited file lists each record as follows:  
LastName | FirstName | PropertyType | Project | StartDate | Phone  

• The comma-delimited file looks like this:  
LastName, FirstName, PropertyType, Project, StartDate, Phone  

• The space-delimited file looks like this:  
LastName FirstName PropertyType Project StartDate Phone  
  
### Output  

Create and display 3 different views of the data you read in:  
• Output 1 – sorted by property type (houses, condos, trailers) then by project ascending.  • Output 2 – sorted by start date, ascending.  
• Output 3 – sorted by last name, descending.   

## Step 2 - Build a REST API to access your system  
 
Within the same code base, build a standalone REST API with the following endpoints:  

• POST /leads - Post a single data line in any of the 3 formats supported by your existing code  
• GET /leads/propertytype - returns leads sorted by property type  
• GET /leads/startdate - returns leads sorted by start date  
• GET /leads/project - returns leads sorted by project  
• GET /leads/duplicates/ - returns leads that are duplicated when compared with leads from an  external source.

## Publish Console App
Open terminal and navigate to the root dir then run:

```csc Customer.App/*.cs Customer/*.cs Customer.TextParser/*.cs```

## Run Console App
Open terminal and navigate to the root dir then run:

```mono CustomerLeadsApp.exe```

Usage: CustomerLeadsApp [path-to-file] [parser-options] [command] [command-option]

path-to-file:

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;The path to the file to be parsed

parser-options:

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;--pipe&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Text delimiter "|"

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;--comma&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Text delimiter ","

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;--space&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Text delimiter " "

commands:

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;sort&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;sorts the customer collection

command-options:

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;--property-type

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;--start-date

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;--last-name

There are sample txt files located at ./samples that you can use. The examples below make use of such samples. Open terminal and navigate to the root dir then run:

```mono CustomerLeadsApp.exe samples/comma-customer.txt --comma sort --startdate```

```mono CustomerLeadsApp.exe samples/pipe-customer.txt --pipe sort --lastname```

```mono CustomerLeadsApp.exe samples/space-customer.txt --space sort --property-type```

## Set Api Configuration 
Open Customer.Api/appsettings.{Environment}.json. Add value for LeadsApiKey.

``` "LeadsApiKey": "[api key]" ```

## Run Api
Open terminal and navigate to the root dir then run:

```dotnet run --project Customer.Api```

Open web browser and navigate to:

``` https://localhost:5001/swagger/index.html ```

## Test Solution
Open terminal and navigate to the root dir then run:

```dotnet test```