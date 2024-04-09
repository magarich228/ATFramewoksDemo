from atclient import AtClient
import json
from types import SimpleNamespace

class AtclientLibrary(object):
    def __init__(self):
        self._atclient = AtClient()

    def invoke_test(self):
        self._atclient.invokeATRandomTest()

    def response_should_be_success(self):
        data = self._atclient._response.read().decode()
        dataObj = json.loads(data, object_hook=lambda d: SimpleNamespace(**d))
        
        if dataObj.TestResult != "Success":
            raise AssertionError(f"TestResult: {dataObj.TestResult}")