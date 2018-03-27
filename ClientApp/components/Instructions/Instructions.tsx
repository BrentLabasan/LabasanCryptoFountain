import * as React from 'react';

export interface Props {
    tokenName: string;
    // enthusiasmLevel?: number;
  }

export default class Instructions extends React.Component<Props> {
    public render() {
        return <div>
            <h1>Instructions To Receive {this.props.tokenName} Tokens</h1>
            <p></p>
        </div>;
    }
}
