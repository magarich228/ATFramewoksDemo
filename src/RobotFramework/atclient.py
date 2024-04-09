import http.client
import uuid


class AtClient(object):
    def __init__(self) -> None:
        pass

    def invokeATRandomTest(self):
        host = "localhost:29300"
        conn = http.client.HTTPConnection(host)
        headers = {'Content-type': 'application/json'}

        conn.request("POST",
                     f"/inias/csc/autotests/auto-tests/service/auto-tests/execute/{uuid.uuid4()}/CorLibBundleAutoTestRandomExample/http,npgsql,kafka",
                     headers=headers)

        self._response = conn.getresponse()
