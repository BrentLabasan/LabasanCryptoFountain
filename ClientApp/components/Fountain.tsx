import * as React from 'react';
import { RouteComponentProps } from 'react-router-dom';
import * as StellarSdk from 'stellar-sdk';

import { Tabs, Tab } from 'react-bootstrap';
import Instructions from './Instructions/Instructions';

interface IState {
    tokenName: string;
    issuerAccountId: string;
    address?: string;
    addressIsValid: boolean;
}

interface IProps extends RouteComponentProps<{}> {
    address?: string;
    addressIsValid?: boolean;
    meow: (address: string) => any;
}

export default class Fountain extends React.Component<IProps, IState>  {
    constructor(props: IProps) {
        super(props);
        // set initial state
        this.state= {
            tokenName: "",
            issuerAccountId: "",
            address: "",
            addressIsValid: false
        }
    }

    meow = (address: string) => {
        // console.log(address);

        if (StellarSdk.StrKey.isValidEd25519PublicKey(address)) {
            this.setState({addressIsValid: true});
            // console.log("corr")
            let server = new StellarSdk.Server('https://horizon.stellar.org');
            server.accounts()
            .accountId(address)
            .call().then(function (r) { console.log(r); });
            this.forceUpdate();
        } else {
            console.log("inv")
            this.setState({addressIsValid: false});

          }

        this.setState({address: address});
    }
    
    
    public render() {
        return <div>
            <Tabs defaultActiveKey={2} id="uncontrolled-tab-example">
                <Tab eventKey={1} title="SECOND">
                    <Instructions tokenName="SECOND" issuerAccountId="GAYZT6ZQCWRSUYUYKTTMX2BACITUQRXZPBXLY7H5PJ4WUNJU6ZET42W5" address={this.state.address} addressIsValid={this.state.addressIsValid} meow={this.meow} />
                </Tab>
                <Tab eventKey={2} title="MINUTE">
                    <Instructions tokenName="MINUTE" issuerAccountId="GCLU3JPHTGA6KTWP77VZ44VJKCYA2K2F7CDLWBYL5KQQ6XC27F44XWGM" address={this.state.address} addressIsValid={this.state.addressIsValid} meow={this.meow} />
                </Tab>
                <Tab eventKey={3} title="HOUR">
                    <Instructions tokenName="HOUR" issuerAccountId="GA2L7HVLDVJ76HKSPGR3IRD2ZX2AWHRPGA7PKAEGNUIAPKIL72GX5UEG" address={this.state.address} addressIsValid={this.state.addressIsValid} meow={this.meow} />
                </Tab>
                <Tab eventKey={4} title="DAY">
                    <Instructions tokenName="DAY" issuerAccountId="GAELLKQPLBQVZZURK7JT45U22RQIU2DN57HPGBOGQYJLPGN25NEZKT7U" address={this.state.address} addressIsValid={this.state.addressIsValid} meow={this.meow} />
                </Tab>
                <Tab eventKey={5} title="WEEK">
                    <Instructions tokenName="WEEK" issuerAccountId="GAKMD63LOOLWXNJOQPIZ6IF2MW3ABHT2N6RE4H3ECU57JQMUGDIBP5X2" address={this.state.address} addressIsValid={this.state.addressIsValid} meow={this.meow} />
                </Tab>
                <Tab eventKey={6} title="MONTH">
                    <Instructions tokenName="MONTH" issuerAccountId="GAHOPLW6VEXFCZK4PC3RZHDQIASRA4NM7IUXLBSLNUUZ2CL4F5ITYDLF" address={this.state.address} addressIsValid={this.state.addressIsValid} meow={this.meow} />
                </Tab>
                <Tab eventKey={7} title="YEAR">
                    <Instructions tokenName="YEAR" issuerAccountId="GAHE3PVC4QE5TBOMXKVOQF56ZMOPSDJ6WSLQJPDNSPROVRCXRBUPGLFU" address={this.state.address} addressIsValid={this.state.addressIsValid} meow={this.meow} />
                </Tab>
            </Tabs>
        </div>;
    }
}