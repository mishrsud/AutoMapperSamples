# AutoMapperSamples

## Synopsis
Code Samples that demonstrate usage of the popular AutoMapper Library

Major Use cases:
1) Map an Object's properties with data retrieved from a database using an IDataRecord
2) Object-Object mapping using the global, static Mapper
3) Create and use configurations and instance based mappers

## Tests

**NOTE** MS SQL Express 2014 is required to run tests. Get your copy from here: http://www.microsoft.com/en-in/download/details.aspx?id=42299
IF YOU NAME THE INSTANCE ANYTHING OTHER THAN SQLEXPRESS, REMEMBER TO UPDATE THE app.config IN TEST

To run test, Rebuild the solution first to restore NuGet packages. The following NuGet packages are required:


|	Package Id						| Version			|
| --------------------------------|-----------------|
| Microsoft.ApplicationBlocks.Data 	| Version 2.0.0		|
| NUnit								| Version 2.6.4		|
| FluentAssertions					| Version 3.3.0		|

## Contributors

@sudhanshutheone.

## License

Open Source!