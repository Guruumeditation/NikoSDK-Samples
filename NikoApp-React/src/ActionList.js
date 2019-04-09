import React, { Component } from 'react';
import Switch from 'rc-switch';

const ActionList = (props) => {
    const [currentrequests, setcurrentrequests] = React.useState([]);

    var actions = props.actions;

    const onExecuteAction = async (actionid, value) => {
	    if (currentrequests.includes(actionid))
		    return;
	    console.log(`Start Execute Action id ${actionid} value : ${value}`)
	    setcurrentrequests(currentrequests.concat(actionid));
	    await executeAction(actionid, value);
	    console.log("End Execute Action");
	    setcurrentrequests(currentrequests.filter(a => a !== actionid));
    };

    return (
        <div className="container">
            <div className="row">
                <div className="col-sm-auto">
                    Id
                </div>
                <div className="col-sm">
                    Name
                </div>
                <div className="col-sm">
                    Value
                </div>
                <div className="col-sm">
                     Location
                </div>
            </div>
            {actions.map(action => <Action key={action.id} onExecuteAction={onExecuteAction} {...action}/>)}
        </div >
    );
};

async function executeAction(actionid, value) {

    var json = JSON.stringify({ "cmd": "executeactions", "id": actionid, "value" : value  });
    var response = await fetch("https://localhost:44307/api/actions",
        {
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'cache' : "no-store"
            },
            method: "POST",
            body: json
        });

}

class Action extends Component {

    render() {
        const action = this.props;

        return (
            <div className="row">
                <div className="col-sm-auto">
                    {action.id}
                </div>
                <div className="col-sm">
                    {action.name}
                </div>
                <div className="col-sm">
                    <Switch checkedChildren="On" unCheckedChildren="Off" onChange={(v, e) => action.onExecuteAction(action.id, v ? 100 : 0)} checked={action.value == 100}  />
                </div>
                <div className="col-sm">
                    {action.locationName}
                </div>
            </div>
            );
    }
}

export default ActionList;