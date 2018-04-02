import * as React from 'react';
import { NavLink, Link } from 'react-router-dom';

export class NavMenu extends React.Component<{}, {}> {
    public render() {
        return <div className='main-nav'>
                <div className='navbar navbar-inverse'>
                <div className='navbar-header'>
                    <button type='button' className='navbar-toggle' data-toggle='collapse' data-target='.navbar-collapse'>
                        <span className='sr-only'>Toggle navigation</span>
                        <span className='icon-bar'></span>
                        <span className='icon-bar'></span>
                        <span className='icon-bar'></span>
                    </button>
                    <Link className='navbar-brand' to={ '/' }>
                    {/* <Link className='navbar-brand' style={{height: 85}} to={ '/' }> */}
{/*                     
                    LABASAN <br/>
                    CRYPTO <br/>
                    FOUNTAIN <br/> */}

                    <span style={{fontSize: 15, display: "block", overflow: "hidden"}}>LABASAN.CRYPTO.FOUNTAIN</span>
                    </Link>
                </div>
                <div className='clearfix'></div>
                <div className='navbar-collapse collapse'>
                    <ul className='nav navbar-nav'>

                        <li>
                            <NavLink exact to={ '/' } activeClassName='active'>
                                <span className='glyphicon glyphicon-home'></span> Home
                            </NavLink>
                        </li>

                        <li>
                            <NavLink exact to={ '/fountain' } activeClassName='active'>
                                <span className='glyphicon glyphicon-ok'></span> Fountain
                            </NavLink>
                        </li>
                        <li>
                            <NavLink exact to={ '/about' } activeClassName='active'>
                                <span className='glyphicon glyphicon-ok'></span> About
                            </NavLink>
                        </li>

                        {/* <li>
                            <NavLink to={ '/counter' } activeClassName='active'>
                                <span className='glyphicon glyphicon-education'></span> Counter
                            </NavLink>
                        </li>
                        <li>
                            <NavLink to={ '/fetchdata' } activeClassName='active'>
                                <span className='glyphicon glyphicon-th-list'></span> Fetch data
                            </NavLink>
                        </li> */}

                    </ul>
                </div>
            </div>
        </div>;
    }
}
