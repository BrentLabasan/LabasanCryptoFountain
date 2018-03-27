import * as React from 'react';

export interface Props {
    tokenName: string;
    issuerAccountId: string;
    // enthusiasmLevel?: number;
}

export default class Instructions extends React.Component<Props> {
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
<input type="text" />
                        </li>
                    </ul>
                </li>
            </ol>
        </div>;
    }
}
