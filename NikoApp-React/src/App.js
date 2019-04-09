import React, { Component } from 'react';
import './App.css';
import './switch.css';
import ActionList from  './ActionList.js'
import { HubConnectionBuilder} from '@aspnet/signalr'

class App extends Component {
    state = {
        actions: [],
        locations : []
    }


    connectionSignalR = new HubConnectionBuilder().withUrl("https://localhost:44307/NikoEventHub").build();

    componentDidMount = () => {
        this.connectionSignalR.on("ReceiveEvent", () => this.loadActions());
        this.connectionSignalR.start();
    }

    loadActions = async () => {
        var actionspromise = fetch("https://localhost:44307/api/actions", {mode: "cors"});
        var locationpromise = fetch("https://localhost:44307/api/locations", { mode: "cors" });
        var [result1, result2] = await Promise.all([actionspromise, locationpromise]);

        var actions = await result1.json();
        var locations = await result2.json();

        actions.forEach((part, index) => {
                var location = locations.find(l => l.id == part.locationId);
                part.locationName = location.name;
            },
            actions);

        this.setState({ actions: actions });
        this.setState({ locations : locations });
    }

  render() {
    return (
        <div className="App container">
            <h2>Niko Home Control Actions</h2>
            <button className="btn btn-primary" onClick={this.loadActions}>Load</button>
            <ActionList actions={this.state.actions} reloadActions={this.loadActions} />
      </div>
    );
  }
}

export default App;
