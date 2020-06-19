import React from 'react';
import NavMenu from './NavMenu';

export const Layout = (props: React.HTMLAttributes<HTMLElement>) => {
    return (
        <div>
            <NavMenu />
            <div id="content-container">
                {props.children}
            </div>
        </div>
    );
}
