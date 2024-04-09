from pythonnet import load

load("coreclr")

import clr
import sys

assembly_path = r"..\AT\AT.NUnitTests.NET5\bin\Debug\net5.0"
sys.path.append(assembly_path)

clr.AddReference("AT.NUnitTests.NET5")

from AT.NUnitTests.NET5 import AtClientTests


class AtclientLibraryNet(object):
    def __init__(self):
        self.atclient_tests = AtClientTests()
        self.atclient_tests.Setup()

    def random_test(self):
        self.atclient_tests.RandomTest()

    def two_params_test(self):
        self.atclient_tests.TwoParamsTest()
        
    def pass_test(self):
        self.atclient_tests.TestPass()