import Oidc from 'oidc-client';

var mgr = new Oidc.UserManager({
    authority: 'http://localhost:5000',
    client_id: 'frontend',
    redirect_uri: 'http://localhost:8081/callback',
    response_type: 'code',
    scope: 'openid profile backend',
    post_logout_redirect_uri: 'http://localhost:8081',
    userStore: new Oidc.WebStorageStateStore({ store: window.localStorage }),
})

Oidc.Log.logger = console;
Oidc.Log.level = Oidc.Log.INFO;

export default mgr;