

## Add A Catalog Item

POST /vendors/{id}/catalog-items


# In the beginning there was no "standard" way to "document" an API.

Started an open source joint called "Swagger"

- A way to use a rule-bound JSON document to describe in a standard way an API. (OAS - OpenApi Specification)
- SwaggerUi - that can display a swagger json document.
- Swagger Studio - HORRIBLE ABOMINATION 
- Code Generators - (NSwag)


## "REST" architectural style

- Resources
- Http Methods
- Representations
- Hypermedia Affordances
    - LINKS.

GET /employees/tricia
GET /employees/bob-smith/manager

{
    id: "barb-jones",
    name: "Bob Smith",
    department: "DEV",
    links: {
        "self": "/employees/barb-jones",
        "manager": "/employees/tricia",
        "team": "/employees?department=DEV"
    }
}

GET /

{
    links: {
        "self": "...",
        "employes": "/employees",
        "products": "/products"
    }
}

JSON.api - a format doing this.