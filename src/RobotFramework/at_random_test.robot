*** Settings ***
Documentation     Example test cases using the keyword-driven tests with ATClient.
Library           AtclientLibrary.py

*** Test Cases ***
Invoke Test 1
    Invoke Random Test
    Response Should Be Success

Invoke Test 2
    Invoke Random Test
    Response Should Be Success

Invoke Test 3
    Invoke Random Test
    Response Should Be Success

*** Keywords ***
Invoke Random Test
    Invoke Test

Reponse Should Be Success
    Response Should Be Success