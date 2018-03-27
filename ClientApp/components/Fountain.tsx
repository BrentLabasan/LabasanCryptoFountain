import * as React from 'react';
import { Link, RouteComponentProps } from 'react-router-dom';
import { connect } from 'react-redux';
import { ApplicationState } from '../store';
import * as CounterStore from '../store/Counter';
import * as WeatherForecasts from '../store/WeatherForecasts';

import { Tabs, Tab } from 'react-bootstrap';

type CounterProps =
    CounterStore.CounterState
    & typeof CounterStore.actionCreators
    & RouteComponentProps<{}>;

class Counter extends React.Component<CounterProps, {}> {
    public render() {
        return <div>
            <Tabs defaultActiveKey={2} id="uncontrolled-tab-example">
                <Tab eventKey={1} title="SECOND">
                    Tab 1 content
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

// Wire up the React component to the Redux store
export default connect(
    (state: ApplicationState) => state.counter, // Selects which state properties are merged into the component's props
    CounterStore.actionCreators                 // Selects which action creators are merged into the component's props
)(Counter) as typeof Counter;