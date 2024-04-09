*** Settings ***
Documentation     Example test cases using the keyword-driven tests with ATClient.
Library    Remote    http://127.0.0.1:8270/NRobot/Server/Test/Keywords/AtClientKeywords

*** Test Cases ***
Invoke Random Test1
    Remote.INVOKE RANDOMTEST
    Remote.RESPONSE STATUSCODEIS    ,500
    Remote.RESPONSE TESTRESULTIS    Success