using Reqnroll;
using RestSharp;
using Shouldly;
using WDI2026.Presentation.API.Client;
using WDI2026.Presentation.API.Client.Models;
using WDI2026.Presentation.API.Common.Logging.Interfaces;
using Product = WDI2026.Presentation.API.Client.Models.Product;

namespace WDI2026.Presentation.API.Tests.Steps
{
    [Binding]
    internal sealed class ProductsSteps(ApiClient apiClient, IStepsLogger stepsLogger)
    {
        private Product _product;
        private RestResponse<Created> _createdResponse;

        [Given(@"a new product with name ""(.*)"", description ""(.*)"", price (.*) and stock (.*)")]
        public void GivenANewProductWithNameDescriptionPriceAndStock(string name, string description, double price, int stock)
        {
            _product = new Product
            {
                Name = name,
                Description = description,
                Price = price,
                Stock = stock
            };
        }

        [When(@"I create the product")]
        public async Task WhenICreateTheProduct()
        {
            _createdResponse = await apiClient.CreateProductResponse(_product);
        }

        [When(@"I attempt to create the product")]
        public async Task WhenIAttemptToCreateTheProduct()
        {
            await WhenICreateTheProduct();
        }

        [Then(@"the product should be created successfully and retrieving it should match the original")]
        public async Task ThenTheProductShouldBeCreatedSuccessfullyAndRetrievingItShouldMatchTheOriginal()
        {
            _createdResponse.ShouldNotBeNull();

            var retrieved = await apiClient.GetProduct(_createdResponse.Data.Id);

            retrieved.ShouldSatisfyAllConditions(
            () => retrieved.ShouldNotBeNull(),
            () => retrieved.Name.ShouldBe(_product.Name),
            () => retrieved.Description.ShouldBe(_product.Description),
            () => retrieved.Price.ShouldBe(_product.Price),
            () => retrieved.Stock.ShouldBe(_product.Stock));
        }

        [Given(@"the product is created")]
        public async Task GivenTheProductIsCreated()
        {
            await WhenICreateTheProduct();
            _createdResponse.ShouldNotBeNull();
        }

        [When(@"I update the product description to ""(.*)""")]
        public async Task WhenIUpdateTheProductDescriptionTo(string newDescription)
        {
            _product.Description = newDescription;

            var putResponse = await apiClient.UpdateProduct(_createdResponse.Data.Id, _product);
        }

        [Then(@"retrieving the product should return description ""(.*)""")]
        public async Task ThenRetrievingTheProductShouldReturnDescription(string expectedDescription)
        {
            _createdResponse.ShouldNotBeNull();
            var retrieved = await apiClient.GetProduct(_createdResponse.Data.Id);
            retrieved.ShouldNotBeNull();
            retrieved.Description.ShouldBe(expectedDescription);
        }

        [Then(@"the create response should be unsuccessful")]
        public void ThenTheCreateResponseShouldBeUnsuccessful()
        {
            _createdResponse.ShouldNotBeNull();
            _createdResponse.IsSuccessful.ShouldBeFalse();
        }
    }
}