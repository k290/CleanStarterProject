import React from 'react';
import { Menu, MenuItem, MenuSelectEvent } from '@progress/kendo-react-layout';
import { withRouter, RouteComponentProps } from 'react-router-dom';

const NavMenu = (props: RouteComponentProps) => {

    const onSelect = (event: MenuSelectEvent) => {
        props.history.push(event.item.data.route);
    }

    return (
        <header>
            <Menu onSelect={onSelect}>
                <MenuItem text="Movies" data={{ route: '/' }} />
                <MenuItem text="Actors" data={{ route: '/actors' }} />
            </Menu>
        </header>
    );
}
export default withRouter(NavMenu);