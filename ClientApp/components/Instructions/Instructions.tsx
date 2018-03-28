import * as React from 'react';
import { Button } from 'react-bootstrap';
import * as StellarSdk from 'stellar-sdk';

import * as FontAwesome from 'react-icons/lib/md'

interface IProps extends IState {
    tokenName: string;
    issuerAccountId: string;
    address?: string;
    addressIsValid?: boolean;
    canAcceptToken?: boolean;

    // meow: (address: string) => any;
    selectedToken: string;

}

interface IState {
    address?: string;
    addressIsValid?: boolean;
    hasEnoughXlm?: boolean;
    canAcceptToken?: boolean;
}

export default class Instructions extends React.Component<IProps, IState> {

    constructor(props: IProps) {
        super(props);
        // set initial state

        this.state = {
            address: this.props.address,
            addressIsValid: this.props.addressIsValid,
            hasEnoughXlm: this.props.hasEnoughXlm,
            canAcceptToken: this.props.canAcceptToken
        }
    }

    meow = (address: string) => {
        // console.log(address);

        if (StellarSdk.StrKey.isValidEd25519PublicKey(address)) {
            this.setState({ addressIsValid: true });
            localStorage.setItem('lastEnteredAddress', address);
            // console.log("corr")
            let server = new StellarSdk.Server('https://horizon.stellar.org');
            server.accounts()
                .accountId(address)
                .call().then((r) => {
                    console.log(r);

                    // console.log(typeof r);
                    // console.log(Object.getOwnPropertyNames(r));
                    // console.log(JSON.stringify(r));
                    // console.log(JSON.parse(JSON.stringify(r)));
                    let result = JSON.parse(JSON.stringify(r));
                    // console.log("result.id", result.id);
                    // console.log("result.balances", result.balances);

                    // alert(result.id);

                    if (result.balances[result.balances.length - 1].balance >= 4.5) {
                        this.setState({ hasEnoughXlm: true });
                        // alert("more than 4.5");

                    } else {
                        this.setState({ hasEnoughXlm: false });
                        // alert("less than 4.5");
                    }


                    // I moved this entire chunk from outter to inner. Not 100% sure if that was correct.
                    let canAcceptToken = false;
                    result.balances.forEach((b: any) => {
                        if (b.asset_code) {
                            // console.log("typeof b.asset_code", typeof b.asset_code);
                            console.log("compare balances accepted vs. tab's token", this.props.selectedToken.toUpperCase(), b.asset_code.toUpperCase());
                            if (this.props.selectedToken.toUpperCase() === b.asset_code.toUpperCase()) {
                                canAcceptToken = true;
                                // There's no built-in ability to break in forEach. https://stackoverflow.com/a/2641374
                            }
                        }
                    });
                    console.log(canAcceptToken);
                    if (canAcceptToken) {
                        this.setState({ canAcceptToken: true });
                    } else {
                        this.setState({ canAcceptToken: false });
                    }



                });
        } else { // if query entered into field isn't a valid public key
            console.log("query entered into field isn't a valid public key")
            this.setState({ addressIsValid: false, hasEnoughXlm: false, canAcceptToken: false });
        }

        this.setState({ address: address });
    }



    addressFieldChange = (e: React.FormEvent<HTMLInputElement>) => {
        // console.log(e.currentTarget.value)
        // this.setState({address: e.currentTarget.value});
        this.meow(e.currentTarget.value.toUpperCase());

    }

    handleClick = () => {
        alert();
        let server = new StellarSdk.Server('https://horizon.stellar.org');

        // server.transactions()
        // .forAccount('GASOCNHNNLYFNMDJYQ3XFMI7BYHIOCFW3GJEOWRPEGK2TDPGTG2E5EDW')
        // .call().then(function(r){ console.log(r); });

        server.accounts()
            .accountId("GB4P2YXKH3IYUKCBEATQ75EX7BOPWC6HPABUZVW7UNODXKH6AVWDIL3D")
            .call().then(function (r) { console.log(r); });

        // server.accounts
        // console.log(server.accounts().accountId("GAQ4HHIYU6BQEMUBFIJA7QMXSNHNQDGPD45D4HAWGLWJWBAMUWJ6BOSC"));

    }

    // componentWillMount() {
    //     console.log("componentWillMount");
    //     if (!this.state.address) {
    //         if (localStorage.getItem("lastEnteredAddress")) {
    //             this.setState({ address: localStorage.getItem("lastEnteredAddress") || "" });
    //         }
    //     }
    // }

    public render() {

        let checkboxStep1 = this.state.addressIsValid ? <FontAwesome.MdCheckBox /> : <FontAwesome.MdCheckBoxOutlineBlank />;
        let checkboxStep2 = this.state.addressIsValid && this.state.hasEnoughXlm ? <FontAwesome.MdCheckBox /> : <FontAwesome.MdCheckBoxOutlineBlank />;
        let checkboxStep3 = this.state.addressIsValid && this.state.hasEnoughXlm && this.state.canAcceptToken ? <FontAwesome.MdCheckBox /> : <FontAwesome.MdCheckBoxOutlineBlank />;
        let finalStep = this.state.addressIsValid && this.state.hasEnoughXlm && this.state.canAcceptToken ? <li><Button bsStyle="success" onClick={this.handleClick}>Receive</Button></li> : <li><Button bsStyle="success" onClick={this.handleClick} disabled>Receive</Button></li>;


        return <div>
            <h1>Instructions To Receive {this.props.tokenName} Tokens</h1>
            <ol>
                <li>{checkboxStep1} Enter your Stellar account's public address/key.
                    <ul>
                        <li>
                            {/* <input type="text" onChange={ e => this.addressFieldChange(e) } value={ this.state.address } /> <Button bsStyle="success" onClick={this.handleClick}>Receive</Button> */}
                            <input style={{width: 600}} placeholder="example: GCDMFH3RSZR3FLBHSUYPLF2XAG5TWZQDHNX5XG4UELVXICNBESDFMXTJ" type="text" onChange={this.addressFieldChange} value={this.state.address} />
                        </li>
                    </ul>
                </li>

                <li>{checkboxStep2} Make sure that your Stellar account has enough XLM in it to support base fees.
                    <ul>
                        {/* https://www.stellar.org/developers/guides/concepts/fees.html#minimum-account-balance */}
                        <li>Each additional XLM-based token you add as a trustline to your account requires .5 XLM.
                            So to be able to add all Time Saved Tokens, have <b>at least 4.5 XLM in your account</b>.
                        </li>
                    </ul>
                </li>

                <li>{checkboxStep3} Allow your Stellar account to accept {this.props.tokenName} tokens.
                    <ul>
                        <li>Asset Code: {this.props.tokenName}</li>
                        <li>Issuer Account ID: {this.props.issuerAccountId}</li>
                    </ul>
                </li>

                {finalStep}

            </ol>
            {/* this.state.address: {this.state.address} */}
        </div>;
    }
}
