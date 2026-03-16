@Products
@Dev @Ppe 
Feature: Products
  As a QA engineer
  I want to verify that the Products API supports CRUD operations
  So that I can ensure the API handles product management correctly and validates input properly

Scenario: Create product and verify it can be retrieved
	Given a new product with name "ProductTestWDI2026", description "TestProductWDI2026", price 44 and stock 3
	When I create the product
	Then the product should be created successfully and retrieving it should match the original

Scenario: Update product description and verify update
	Given a new product with name "ProductTestWDI2026", description "TestProductWDI2026", price 44 and stock 3
	And the product is created
	When I update the product description to "New description for ProductTestWDI2026"
	Then retrieving the product should return description "New description for ProductTestWDI2026"

Scenario: Creating invalid product returns validation error
	Given a new product with name "ProductTestWDI2026", description "TestProductWDI2026", price -1 and stock 13
	When I attempt to create the product
	Then the create response should be unsuccessful