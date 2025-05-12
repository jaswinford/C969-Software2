## 2025-05-12
All of the database objects contain several overlapping properties:
- id
- createDate
- createdBy
- lastUpdate
- lastUpdateBy

I should create a generic class for database objects and make child classes for each specific type of database object.

### 19:22

Created DBObject class to act as a base for all Class-based representations of DB Objects.

Converted existing classes to partial classes that inherit the DBObject class.
