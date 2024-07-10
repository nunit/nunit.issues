# Introduce HookExtension to support high level tests

Demo test for [Issue 4744](https://github.com/nunit/nunit/issues/4744) and the related draft [pull request](https://github.com/nunit/nunit/pull/4745).

The test is using NUnitLite and therefore you get an executable for test execution. At the end, a log is written to the console in order to understand the interaction between the test and HookExtension using a small web store example.

## Important files to look at

The hook extension itself is defined in ```HookExtension.cs```. This is used to implement reusable test environments between different high-level tests:

* ```[OneTimeSetUp]``` \ ```[OneTimeTearDown]``` Level: Set up the required database and enable all admin-relevant preparations and cleanups.

* ```[SetUp]``` \ ```[TearDown]``` level: Set up a test customer and enable the appropriate methods for all customer-related preparation and cleanup.

The ```HookExtension.cs``` file also defines an ```IApplyToContext``` attribute that will enable the hook extension in the ```AssemblyInfo.cs``` file.

The ``WebShopDemoTest`` test in the corresponding file can then focus on preparing the relevant data for the web applications in the various SetUps and TearDowns and on the test itself.

## Console Output as a table

| Method | Message |
| ------ | ------- |
| BeforeAnySetUps | **[Test Environment]** Initiate Database (on demand), load schemas (on demand), login as admin |
| PrepareProductCatalog | _[OneTimeSetUp]_ add some products to the selling catalog |
| BeforeAnySetUps | **[Test Environment]** Create Customer User and login |
| PrepareShoppingCart | _[SetUp]_ Set delivery address and payment method for customer user |
| BeforeTest | **[Test Environment]** Open shopping application as a common starting point for all tests |
| SomeTest | _[Test]_ some testing in the created environment |
| AfterTest | **[Test Environment]** Close shopping application |
| CleanUserRelatedDataInOtherApps | _[TearDown]_ clean up custom user related data in other sub systems like payment or delivery |
| AfterAnyTearDowns | **[Test Environment]** Log off and remove Customer User |
| BeforeAnyTearDowns | **[Test Environment]** login as admin |
| CleanProductCatalog | _[OneTimeTearDown]_ clean up product catalog |
| AfterAnyTearDowns | **[Test Environment]** log off as admin |
