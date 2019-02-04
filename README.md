[![possumlabs-specflow-core MyGet Build Status](https://www.myget.org/BuildSource/Badge/possumlabs-specflow-core?identifier=3f70eece-3656-4c04-83b0-7474590e0c0e)](https://www.myget.org/)

# PossumLabs.Specflow.Core
The core library for the Possum Labs Specflow extention

Please see https://github.com/BasHamer/PossumLabs.Specflow for tutorials and examples.

## Variables
Scenario: Variables
  Given the Companies
  | Var           |
  | ------------- |
  | C1            |
  And the Users
  | Var           | Company       |
  | ------------- |:-------------:|
  | U1            | C1            |
  | U2            | C1            |


## Validation

Scenario: Promotion
	Given the Employee
	| var | Name | 
  | --- |:----:|
	| E1  | Bob  | 
	And the Employee
	| var | Name | Reports |
  | --- |:----:|:-------:|
	| E2  | Mary | E1      |
	And the root Employee is 'E2'
	When Employee 'E2' Retires
	Then 'E1.Role' has the value 'CEO'
