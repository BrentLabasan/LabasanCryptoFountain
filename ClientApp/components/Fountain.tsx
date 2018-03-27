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
                        Tab 2 content
                </Tab>
                <Tab eventKey={3} title="HOUR">
                        Tab 3 content
                </Tab>
                <Tab eventKey={4} title="DAY">
                        Tab  content
                </Tab>
                <Tab eventKey={5} title="WEEK">
                        Tab  content
                </Tab>
                <Tab eventKey={6} title="MONTH">
                        Tab  content
                </Tab>
                <Tab eventKey={7} title="YEAR">
                        Tab  content
                </Tab>
            </Tabs>
        </div>;
    }
}