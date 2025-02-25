﻿using GraphQL;
using GraphQL.Types;
using ShoppingGraphQL.Interfaces;
using ShoppingGraphQL.Models;
using ShoppingGraphQL.Type;

namespace ShoppingGraphQL.Mutation
{
    public class CategoryMutation : ObjectGraphType
    {
        public CategoryMutation(ICategoryRepository categoryRepository)
        {
            Field<CategoryType>("CreateCategory").Arguments(new QueryArguments(new QueryArgument<CategoryInputType> { Name = "category" })).Resolve(context =>
            {
                return categoryRepository.AddCategory(context.GetArgument<Category>("category"));
            });

            Field<CategoryType>("UpdateCategory").Arguments(new QueryArguments(new QueryArgument<IntGraphType> { Name = "categoryId" },
                new QueryArgument<CategoryInputType> { Name = "category" })).Resolve(context =>
            {
                var category = context.GetArgument<Category>("category");
                var categoryId = context.GetArgument<int>("categoryId");
                return categoryRepository.UpdateCategory(categoryId, category);
            });

            Field<StringGraphType>("DeleteCategory").Arguments(new QueryArguments(new QueryArgument<IntGraphType> { Name = "categoryId" })).Resolve(context =>
               {

                   var categoryId = context.GetArgument<int>("categoryId");
                   categoryRepository.DeleteCategory(categoryId);
                   return "The category against this Id" + categoryId + "has been deleted";
               });
        }
    }
}
