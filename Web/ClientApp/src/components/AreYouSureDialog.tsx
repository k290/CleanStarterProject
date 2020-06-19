import React, { useState } from 'react';
import { Dialog, DialogActionsBar, DialogCloseEvent } from '@progress/kendo-react-dialogs';

type AreYouSureProps = {
    onYes: (event: React.MouseEvent) => void;
    onNo: (event: React.MouseEvent) => void;
    onClose: (event: DialogCloseEvent) => void;
}

export const AreYouSureDialog = (props: AreYouSureProps) => {



    return (
        <Dialog title={"Please confirm"} onClose={props.onClose} >
            <p style={{ margin: "25px", textAlign: "center" }}>Are you sure?</p>
            <DialogActionsBar>
                <button className="k-button" onClick={props.onNo}>No</button>
                <button className="k-button" onClick={props.onYes}>Yes</button>
            </DialogActionsBar>
        </Dialog>
    );
}