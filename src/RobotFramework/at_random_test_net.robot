*** Settings ***
Documentation     Example test cases using the keyword-driven tests with pythonnet
Library           AtclientLibraryNet.py

*** Test Cases ***
Invoke Rnd Test
    Invoke Random Test

Invoke 2 Params Test
    Invoke Two Params Test

Invoke Rnd and 2 Params Tests
    Invoke Random Test
    Invoke Two Params Test

*** Keywords ***
Invoke Random Test
    Random Test

Invoke Two Params Test
    Two Params Test
    
Invoke Pass Test
    Pass Test