# Pure.RelationalSchema.Random

Random value generators for relational schema components in the **Pure** ecosystem.

[![.NET build & test](https://github.com/kudima03/Pure.RelationalSchema.Random/actions/workflows/build-and-test.yml/badge.svg?branch=main)](https://github.com/kudima03/Pure.RelationalSchema.Random/actions/workflows/build-and-test.yml)
[![Build and Deploy](https://github.com/kudima03/Pure.RelationalSchema.Random/actions/workflows/publish-nuget.yml/badge.svg?branch=main)](https://github.com/kudima03/Pure.RelationalSchema.Random/actions/workflows/publish-nuget.yml)
[![NuGet](https://img.shields.io/nuget/v/Pure.RelationalSchema.Random)](https://www.nuget.org/packages/Pure.RelationalSchema.Random)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)

## Overview

`Pure.RelationalSchema.Random` provides random-value generators for every structural element of a relational schema. These types are intended for use in tests, fuzz testing, and property-based testing scenarios where you need structurally valid but arbitrarily-valued schema objects.

## Types

| Type | Description |
|------|-------------|
| `RandomSchema` | A randomly constructed `ISchema` with a random name, tables, and foreign keys |
| `RandomSchemasCollection` | A random collection of `ISchema` instances |
| `RandomTable` | A randomly constructed `ITable` with random name, columns, and indexes |
| `RandomTablesCollection` | A random collection of `ITable` instances |
| `RandomColumn` | A randomly constructed `IColumn` with a random name and column type |
| `RandomColumnsCollection` | A random collection of `IColumn` instances |
| `RandomColumnType` | A random `IColumnType` |
| `RandomColumnTypesCollection` | A random collection of `IColumnType` instances |
| `RandomForeignKey` | A randomly constructed `IForeignKey` |
| `RandomForeignKeysCollection` | A random collection of `IForeignKey` instances |

All types implement the corresponding `Pure.RelationalSchema.Abstractions` interface and live in the `Pure.RelationalSchema.Random` namespace.

## Dependencies

- [`Pure.RelationalSchema`](https://github.com/kudima03/Pure.RelationalSchema) — concrete schema domain model types
- [`Pure.Primitives.Random`](https://github.com/kudima03/Pure.Primitives.Random) — random primitive value generators
