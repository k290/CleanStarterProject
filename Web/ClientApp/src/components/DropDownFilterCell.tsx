import React from 'react';

import { DropDownList, DropDownListProps, DropDownListChangeEvent, MultiSelect, MultiSelectChangeEvent } from '@progress/kendo-react-dropdowns';
import { GridFilterCellProps, GridCellProps } from '@progress/kendo-react-grid';

export interface IDropdownFilter {

}
export default function multiDropdownFilterCell(data: unknown[] | undefined, defaultItem: unknown, dataItemKey: string, textField: string) {
    return class FilterCell extends React.Component<GridFilterCellProps> implements IDropdownFilter {
        render() {
            return (
                <div className="k-filtercell">
                    <MultiSelect
                        data={data}
                        dataItemKey={dataItemKey}
                        textField={textField}
                        onChange={this.onChange}
                        value={this.props.value}
                    />
                    <button
                        className="k-button k-button-icon k-clear-button-visible"
                        title="Clear"
                        disabled={!this.hasValue(this.props.value)}
                        onClick={this.onClearButtonClick}
                    >
                        <span className="k-icon k-i-filter-clear" />
                    </button>
                </div>
            );
        }

        hasValue = (value: unknown) => Boolean(value && value !== defaultItem);

        onChange = (event: MultiSelectChangeEvent) => {
            const hasValue = this.hasValue(event.target.value);
            if (this.props.onChange) {
                this.props.onChange({
                    value: hasValue ? event.target.value : '',
                    operator: hasValue ? 'eq' : '',
                    syntheticEvent: event.syntheticEvent
                });
            }
        }


        onClearButtonClick = (event: React.MouseEvent<HTMLButtonElement, MouseEvent>) => {
            event.preventDefault();
            if (this.props.onChange) {
                this.props.onChange({
                    value: '',
                    operator: '',
                    syntheticEvent: event
                });
            }

        }
    };
}

