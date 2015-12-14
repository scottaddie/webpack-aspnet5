'use strict';

require('file?name=favicon.ico!./favicon.ico');

import React from 'react';
import { render } from 'react-dom';

const { string } = React.PropTypes;

class App extends React.Component {
	constructor() {
		super();
	}
	
	render() {
		return (
			<div>
				<h1>Hello, {this.props.firstName}{' '}{this.props.lastName}!</h1>
			</div>
		);
	}
}

App.propTypes = {
	firstName: string.isRequired,
	lastName: string.isRequired
};

render(<App firstName="Scott" lastName="Addie" />, document.getElementById('app'));