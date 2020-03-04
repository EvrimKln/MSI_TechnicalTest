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
I assume that in UI more than one vessels are selected, all selected vessels are active(Beacuse we show active vessells to user) 
and built before 2020.
                
I Valuate for each vessel:
  - if the vessel has no valuation
  - if the vessel has valuation but is valuated in previous years.Since the age of vessel changed,it is more healthy to valuate again.
  - if the vessel has valuation but one of the property of the vessel(like size) is changed
  
Since valuation changes depond on vessel type, i avoid to do if- else if blocks which valuate according to vessel type
I apllied open-closed prencible --> i have classses inherited base abstract class for each vessel type
                                --> Each class responsible its valuation, formules, coefficients
                                --> in the future, if we have another valuation for a new vessel type,we just need to create a new class
                                    for valuation and add here, existing classes or valuations will not be affected
                                    
All model has some main properties and methods, so i apllied repository generic pattern.

I have tests to valuate for one vessel and valuate for selected vessel. Test cases compare actual values with expected values.



