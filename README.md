# Project Title

As part of this programming exercise, you will build a backend layer of a Contact Manager application.

## Domain

A Contact is any entity the company does business with.  
The system should maintain the following information about contact.
```
Contact First Name 
Contact Last Name
Contact Business Name
Contact Type (business, or person)
Record Create Date
Record Update Date
```
A contact can have zero or more addresses.  The system should store the following information for the address.
```
Street
City
State
Zip Code
Record Create Date
Record Update Date
```
### Deliverables

* Build an Entity diagram for the data model.
* Generate ANSI SQL DDL for the above information. 
* A .NET class library which allows to do the following
	* Add a contact
	* Delete a contact
	* Add an address
	* Delete an Address
	* Get all contacts.
	* Get contact and address information for a contact id.
	* Return a list of contact for a given zip code.
* Actual Data access using ANSI SQL.
* Unit test of the project.  Assume Database is not available during unit testing.
* A GitHub link for the source code.

### Design considerations

* The user of the class library shall be able to replace this class implementation with their own implementation if needed.
* The user of the class library shall be able to replace "Data Access logi"" with their own implementation if needed.
* This class library can be utilized from ".NET Full Framework" or from ".NET Core" based clients.

### Business Rules

* A contact must have at least "First name and last name" or "Business name".
* An address must have [Street, City, State and Zip Code] specified.
* An address must have a contact.

End with an example of getting some data out of the system or using it for a little demo

## Running the tests

Explain how to run the automated tests for this system

### Break down into end to end tests

Explain what these tests test and why

## Authors

* **Ranjith kumar Muthusamy** - *Initial work* - [Ranjith](https://github.com/ranjithlav)

## TODO

* Unit test for all layers
* Move core functionalities to seperate projects
* Build configurations
