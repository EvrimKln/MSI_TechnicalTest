# MSI_TechnicalTest
This repo contains Valuation Web Api and Test Projects.

-----------------------------------------------------------------------------------------------------------------------------------
Restore packages automatically;
  Enable automatic package restore by choosing Tools > Options > NuGet Package Manager, 
  and then selecting Automatically check for missing packages during build in Visual Studio under Package Restore.
  Build the project.
  
OR

Restore packages manually using Visual Studio
  Enable package restore by choosing Tools > Options > NuGet Package Manager. Under Package Restore options, 
  select Allow NuGet to download missing packages. In Solution Explorer, right click the solution and select Restore NuGet Packages.

-----------------------------------------------------------------------------------------------------------------------------------
I use MicrosoftEntityFrameworkCore --> UseInMemoryDatabase
I seed  InMemory database with test data.

I assume that more than one vessels are selected in in UI, all selected vessels are active(Beacuse we show active vessells to user) 
and built before 2020.
                
I calculate valuations for each vessel:
  - if the vessel has no valuation
  - if the vessel has valuation but is calculated in previous years.Since the age of vessel changed,it is more healthy to calculate      again.
  - if the vessel has valuation but one of the property of the vessel(like size) is changed
  
Since valuation changes depand on vessel type, i avoid to do if- elseif blocks which calculate according to vessel type
I aplied open-closed prencible --> i have inherited classes for each vessel type
                                --> Each class responsible its valuation, formules, coefficients
                                --> in the future, if we have another valuation for a new vessel type,we just need to create a new class
                                    for valuation, existing classes or valuations will not be affected
                                    
All model has some main properties and methods, so i apllied repository generic pattern.

I have tests to calculate for one vessel and calculate for selected vessel. Test cases compare actual values with expected values.



