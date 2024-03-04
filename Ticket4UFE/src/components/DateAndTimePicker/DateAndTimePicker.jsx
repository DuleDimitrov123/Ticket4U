import DatePicker from "react-multi-date-picker";
import InputIcon from "react-multi-date-picker/components/input_icon";
import "../../customCalendar.css";
import { useRef, useState } from "react";
import { useFormikContext } from "formik";

const DateAndTimePicker = () => {
  const [value, setValue] = useState(new Date());
  const { setFieldValue } = useFormikContext();
  const datePickerRef = useRef();
  const changeValue = (value) => {
    setValue(new Date(value));
    setFieldValue("startingDateTime", new Date(value));
  };
  return (
    <DatePicker
      ref={datePickerRef}
      calendarPosition="top"
      render={<InputIcon />}
      value={value}
      onChange={changeValue}
    />
  );
};

export default DateAndTimePicker;
