import Oidc from 'oidc-client';

var mgr = new Oidc.UserManager({
    authority: 'http://192.168.168.172:8086',
    client_id: 'frontend',
    redirect_uri: 'http://192.168.168.172:8085/callback',
    response_type: 'code',
    scope: 'openid profile backend',
    post_logout_redirect_uri: 'http://192.168.168.172:8085',
    userStore: new Oidc.WebStorageStateStore({ store: window.localStorage }),
})

Oidc.Log.logger = console;
Oidc.Log.level = Oidc.Log.INFO;

export default mgr;