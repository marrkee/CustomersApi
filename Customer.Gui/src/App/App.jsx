import React from 'react';
import { HashRouter, Route, Link } from 'react-router-dom';

import { authenticationService } from '@/_services';
import { PrivateRoute } from '@/_components';
import { HomePage } from '@/elements/HomePage';
import { LoginPage } from '@/elements/LoginPage';
import { ManagerPage } from '@/elements/ManagerPage';
import { CustomerPage } from '@/elements/CustomerPage';
import { ManagersPage } from '@/elements/ManagersPage';


class App extends React.Component {
  constructor(props) {
    super(props);

    this.state = {
      currentUser: null,
    };
  }

  componentDidMount() {
    authenticationService.currentUser.subscribe((x) => this.setState({ currentUser: x }));
  }

  logout() {
    authenticationService.logout();
  }

  render() {
    const { currentUser } = this.state;
    return (
      <HashRouter>
        <div>
          {currentUser
                        && (
                        <nav className="navbar navbar-expand navbar-dark bg-dark">
                          <div className="navbar-nav">
                            <Link to="/" className="nav-item nav-link">Home</Link>
                            <Link to="/manager" className="nav-item nav-link">Managers</Link>
                            <Link to="/Login" onClick={this.logout} className="nav-item nav-link">Logout</Link>
                          </div>
                        </nav>
                        )}
          <div style={{ background: 'transparent' }} className="jumbotron">
            <div className="container">
              <div className="row">
                <div className="col-md-6 offset-md-3">
                  <PrivateRoute exact path="/" component={HomePage} />
                  <PrivateRoute exact path="/manager" component={ManagersPage} />
                  <Route path="/login" component={LoginPage} />
                  <PrivateRoute path="/manager/:id" component={ManagerPage} />
                  <PrivateRoute path="/customer/:id" component={CustomerPage} />


                </div>
              </div>
            </div>
          </div>
        </div>
      </HashRouter>
    );
  }
}

export { App };
