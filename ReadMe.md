# TranslateNumbers!

This is a **command line** project based on **.Net Core 3.1**. The main function is to translate an input number to a currency expression. For example: 

> Input: 5.02 

> Output: five Dollars and two Cents

This documentation will contains following parts:

 - Solution Structure
 - Quick Start 
 - Function Assumptions 
 - Unit Testing Report

## Solution Structure
The solution contains two projects. One is **TranslateNumbers** project which has the main function, the other is **TranslationUnitTest** including all unit test cases. 


## Quick Start
1. If you have **visual studio** installed on the laptop, you can open the solution to run it. 
2. If you have .Net Core 3.1 installed, you can build the source code using **command line** and run it.
3. If you want to directly run it. click **TranslateNumbers.exe** file in the published folder.

## Function Assumptions
1. It must be a positive number, up to 2 decimals. 
2. It must be between 0.01 and 999999999999.99 
3. It only accept 0.01, not accept .01 
4. The output of one number is unique. For example, the output of 2200 must be two thousand two hundred Dollars, rather than twenty-two hundred Dollars. 
5. By default, after error or output displaying, it will start to wait input again. 
6. Input "exit" will exit the process.

## Unit Testing Report

1. There are totally 18 unit test cases. All of them are **pass**.
2. The coverage of code in Transaltion.cs (main functions) is **100%**. 

PS: The coverage report is generated by using Coverlet and ReportGenerator packages. Report Path: */coveragereport/TranslateNumbers\_Translation.htm*
Related reference:

https://github.com/tonerdo/coverlet

https://danielpalme.github.io/ReportGenerator/usage.html

