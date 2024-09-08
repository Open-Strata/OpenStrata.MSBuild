
export class DataverseEntityField<TFieldType extends any, TInputs, TOutputs>{

    private _value: TFieldType | null | undefined;
    private _notifyOutputChanged: () => void;
    private _container: HTMLDivElement;
    private _inputElement: HTMLInputElement;
    private _id: string | undefined;
    private _property: ComponentFramework.PropertyTypes.Property;
    private _control: ComponentFramework.StandardControl<TInputs, TOutputs>;
    //private ctx: ComponentFramework.StandardControl

    constructor(control: ComponentFramework.StandardControl<TInputs, TOutputs>, property: ComponentFramework.PropertyTypes.Property, notifyOutputChanged: () => void, container: HTMLDivElement) {
        this._control = control;
        this._notifyOutputChanged = notifyOutputChanged;
        this._container = container;
        this._property = property;
        this._inputElement = document.createElement("input");
        this.id = property.attributes?.LogicalName;
        this.inputElement.type = this.InputType;
        this.refreshData = this.refreshData.bind(this);
        this.addEventListeners();
        this.updateView(property);
        container.appendChild(this.inputElement);
    }

    protected addEventListeners() {
        let events = this.refreshEvents;
        for (let i = 0; i < events.length; i++) {
            this.inputElement.addEventListener(events[i], this.refreshData);
        }
    }

    protected get control(): ComponentFramework.StandardControl<TInputs, TOutputs> {
        return this._control;
    }

    protected get property(): ComponentFramework.PropertyTypes.Property {
        return this._property;
    }

    protected get inputElement(): HTMLInputElement {
        return this._inputElement;
    }



    protected get container(): HTMLDivElement {
        return this._container;
    }

    protected get InputType(): string {
        return "text";
    }

    get id(): string {
        return this._id == undefined ? "" : this._id;
    }

    set id(newValue: string | undefined) {
        this._id = newValue == undefined ? "" : newValue;
        this.inputElement.id = this._id as string;
    }

    get value(): TFieldType | undefined {
        if (this._value == null) return undefined;
        return this._value;
    }

    set value(newValue: TFieldType | null | undefined) {
        this._value = newValue;
        this.setInputValue(newValue);
    }

    protected get refreshEvents(): string[] {
        return [this.refreshEvent];
    }

    protected get refreshEvent(): string {
        return "input";
    }

    protected setAttributes(): void { }

    protected refreshData(evt: Event): void {
        this.value = this.getInputValue();
        this._notifyOutputChanged();
    }

    public updateView(property: ComponentFramework.PropertyTypes.Property): void {
        this._property = property;
        this.value = property.raw as TFieldType | null | undefined;
    }

    protected getInputValue(): TFieldType | null | undefined {
        return this.inputElement.value as TFieldType | null
    }

    protected setInputValue(newValue: TFieldType | null | undefined) {
        this.inputElement.value = this.value == undefined || this.value == null ? "" : this.value as string;
    }

    public setAttribute(attribute: string, value: string) {
        this.inputElement.setAttribute(attribute, value);
    }

    public addEventListener(type: string, listener: EventListenerOrEventListenerObject, options?: boolean | EventListenerOptions | undefined): void {
        this.inputElement.addEventListener(type, listener, options);
    }

    public removeEventListener(type: string, listener: EventListenerOrEventListenerObject, options?: boolean | EventListenerOptions | undefined): void {
        this.inputElement.removeEventListener(type, listener, options);
    }

    public destroy(): void {
        let events = this.refreshEvents;
        for (let i = 0; i < events.length; i++) {
            this.inputElement.removeEventListener(events[i], this.refreshData);
        }
    }
}

export class DataverseDateField<TInputs, TOutputs> extends DataverseEntityField<Date, TInputs, TOutputs>
{
    protected override get InputType(): string {
        return "date";
    }

    protected override setInputValue(newValue: Date | null | undefined): void {
        this.inputElement.valueAsDate = newValue == undefined ? null : newValue;
    }

    protected override getInputValue(): Date | null | undefined {
        return this.inputElement.valueAsDate;
    }
}

export class DataverseWeekField<TInputs, TOutputs> extends DataverseDateField<TInputs, TOutputs>
{
    protected override get InputType(): string {
        return "week";
    }
}

export class DataverseMonthYearField<TInputs, TOutputs> extends DataverseDateField<TInputs, TOutputs>
{
    protected override get InputType(): string {
        return "month";
    }

    protected override setInputValue(newValue: Date | null | undefined): void {

        if (newValue == undefined || newValue == null) {
            this.inputElement.value = "";
        }
        else {
            let dateMonth = newValue.getMonth() + 1;

            this.inputElement.value = newValue.getFullYear() +
                "-" + (dateMonth > 9 ? dateMonth : "0" + dateMonth);
        }
    }

    protected override getInputValue(): Date | null | undefined {
        let thisYearMonth = this.inputElement.value;
        if (!thisYearMonth || thisYearMonth == null || thisYearMonth == "") return null;
        let ymParts = thisYearMonth.split("-");
        return !thisYearMonth || thisYearMonth == "" ? null : new Date(Number(ymParts[0]), Number(ymParts[1]) - 1, 1);
    }

}

export class DataverseNumberField<TInputs, TOutputs> extends DataverseEntityField<number, TInputs, TOutputs>
{
    protected override get InputType(): string {
        return "number";
    }

}