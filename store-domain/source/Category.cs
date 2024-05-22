﻿namespace Store.Domain;

public class Category : TinyType<string>
{
    private Category(string value) : base(value)
    {
    }

    public static Result<Category> Create(string value) =>
        string.IsNullOrWhiteSpace(value)
            ? CategoryRequired()
            : new(value);

    public static Error CategoryRequired() => new($"{nameof(CategoryRequired)} is required.");
}
