# Changelog

All notable changes to Pure.RelationalSchema.Random are documented here.

Format follows [Keep a Changelog](https://keepachangelog.com/en/1.1.0/).

---

## [1.1.1] — 2026-08-04

- Maintenance release: dependency and build updates.

## [1.1.0] — 2026-02-10

### Added

- Package now multi-targets `net7.0`, `net8.0`, `net9.0`, and `net10.0` (previously `net9.0` only).

## [1.0.0] — 2025-09-26

### Changed

- **Breaking:** `RandomForeignKey.ReferencingColumn` (`IColumn`) replaced by `ReferencingColumns`
  (`IEnumerable<IColumn>`); `ReferencedColumn` replaced by `ReferencedColumns` in the same way,
  reflecting multi-column foreign key support.
- **Breaking:** `RandomForeignKey`'s public constructor now takes `RandomColumnsCollection`
  parameters instead of single `RandomColumn` parameters.
- **Breaking:** `RandomForeignKeysCollection`'s constructor now takes
  `IEnumerable<RandomColumnsCollection>` instead of `RandomColumnsCollection` for referencing and
  referenced columns.

## [0.2.0] — 2025-08-31

### Changed

- Default constructors may now generate empty names and empty collections; previously the
  minimum generated length/count was 1.
- Random string lengths generated within a collection are now varied per item instead of shared
  across the whole collection.

## [0.1.0] — 2025-08-29

### Added

- Initial release: random-value generators for every structural element of a relational schema,
  implementing the corresponding `Pure.RelationalSchema.Abstractions` interfaces.
  - `RandomColumnType`, `RandomColumnTypesCollection`
  - `RandomColumn`, `RandomColumnsCollection`
  - `RandomIndex`, `RandomIndexesCollection`
  - `RandomForeignKey`, `RandomForeignKeysCollection`
  - `RandomTable`, `RandomTablesCollection`
  - `RandomSchema`, `RandomSchemasCollection`
- Each type exposes configurable constructors (element counts, name lengths) in addition to
  parameterless defaults.
