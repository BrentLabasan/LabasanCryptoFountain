import * as React from 'react';
import { RouteComponentProps } from 'react-router-dom';

import { Tabs, Tab } from 'react-bootstrap';
import Instructions from './Instructions/Instructions';

export default class Fountain extends React.Component<RouteComponentProps<{}>, {}>  {
    public render() {
        return <div>
            <Tabs defaultActiveKey={2} id="uncontrolled-tab-example">
                <Tab eventKey={1} title="SECOND">
                    <Instructions tokenName="SECOND" />
                </Tab>
                <Tab eventKey={2} title="MINUTE">
                    <Instructions tokenName="MINUTE" />
                </Tab>
                <Tab eventKey={3} title="HOUR">
                <Instructions tokenName="HOUR" />
                </Tab>
                <Tab eventKey={4} title="DAY">
                <Instructions tokenName="DAY" />
                </Tab>
                <Tab eventKey={5} title="WEEK">
                <Instructions tokenName="WEEK" />
                </Tab>
                <Tab eventKey={6} title="MONTH">
                <Instructions tokenName="MONTH" />
                </Tab>
                <Tab eventKey={7} title="YEAR">
                <Instructions tokenName="YEAR" />
                </Tab>
            </Tabs>
        </div>;
    }
}