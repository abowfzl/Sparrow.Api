# Sparrow
API With CQRS(MediatR) Pattern

- [Wrap](https://github.com/abowfzl/Sparrow.Api/tree/master/Sparrow.API/Results/Wrapping) the Api Response and handle [custom Exception](https://github.com/abowfzl/Sparrow.Api/blob/master/Sparrow.API/Exceptions/SparrowException.cs)
with [Exception Handling](https://github.com/abowfzl/Sparrow.Api/blob/master/Sparrow.API/Results/ExceptionHandling/GlobalExceptionFilter.cs)
or [unhandle exception](https://github.com/abowfzl/Sparrow.Api/blob/master/Sparrow.Services/Behaviours/UnhandledExceptionBehaviour.cs)

- Handle [Pagination](https://github.com/abowfzl/Sparrow.Api/blob/master/Sparrow.Core/PagedList.cs) Request and Response in this [folder](https://github.com/abowfzl/Sparrow.Api/tree/master/Sparrow.Core/Pagination)

- Also handle Paged List Async Query in [ToPagedListAsync](https://github.com/abowfzl/Sparrow.Api/blob/master/Sparrow.Data/Extentios/AsyncIQueryableExtensions.cs)

**Maintainer**:
* Abolfazl Moslemian (moslemianAbolfazl@gmail.com)
