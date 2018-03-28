import * as React from 'react';
import { Button } from 'react-bootstrap';
import * as StellarSdk from 'stellar-sdk';
interface IProps {
    tokenName: string;
    issuerAccountId: string;
    address: string;
}

interface IState {
    address: string;
}

export default class Instructions extends React.Component<IProps, IState> {

    constructor(props: IProps) {
        super(props);
        // set initial state

        this.state={address: this.props.address}
    }

    
    addressFieldChange = (e: React.FormEvent<HTMLInputElement>) => {
        console.log(e.currentTarget.value)
    }

    handleClick = () => {
        alert();
        let server = new StellarSdk.Server('https://horizon.stellar.org');

        // server.transactions()
        // .forAccount('GASOCNHNNLYFNMDJYQ3XFMI7BYHIOCFW3GJEOWRPEGK2TDPGTG2E5EDW')
        // .call().then(function(r){ console.log(r); });

        server.accounts()
        .accountId("GB4P2YXKH3IYUKCBEATQ75EX7BOPWC6HPABUZVW7UNODXKH6AVWDIL3D")
        .call().then(function(r){ console.log(r); });

        // server.accounts
        // console.log(server.accounts().accountId("GAQ4HHIYU6BQEMUBFIJA7QMXSNHNQDGPD45D4HAWGLWJWBAMUWJ6BOSC"));

    }

    public render() {
        return <div>
            <h1>Instructions To Receive {this.props.tokenName} Tokens</h1>
            <ol>

                <li>Make sure that your Stellar account has enough XLM in it to support base fees.
                    <ul>
                        {/* https://www.stellar.org/developers/guides/concepts/fees.html#minimum-account-balance */}
                        <li>Each additional XLM-based token you add as a trustline to your account requires .5 XLM.
                            So to be able to add all Time Saved Tokens, have <b>at least 4.5 XLM in your account</b>.
                        </li>
                    </ul>
                </li>
                <li>Allow your Stellar account to accept {this.props.tokenName} tokens.
                    <ul>
                        <li>Asset Code: {this.props.tokenName}</li>
                        <li>Issuer Account ID: {this.props.issuerAccountId}</li>
                    </ul>
                </li>
                <li>Enter your Stellar account's public address/key into the form below and click Receive.
                    <ul>
                        <li>
                            <input type="text" onChange={ e => this.addressFieldChange(e) } value={ this.state.address } /> <Button bsStyle="success" onClick={this.handleClick}>Receive</Button>
                        </li>
                    </ul>
                </li>
            </ol>
        </div>;
    }
}
