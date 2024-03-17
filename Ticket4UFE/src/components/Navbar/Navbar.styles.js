const wrapper = {
  bg: "white",
  color: "gray.600",
  minH: "60px",
  py: 2,
  px: 4,
  borderBottom: 1,
  borderStyle: "solid",
  borderColor: "gray.200",
  align: "center",
  zIndex: 2,
  position: "relative",
  justifyContent: "flex-end",
};

const logo = {
  textAlign: "left",
  fontFamily: "heading",
  fontSize: "3xl",
  lineHeight: "1",
  alignItems: "center",
  width: "48",
  justifyContent: "center",
  color: "purple.300",
};

const logoText = {
  color: "gray.700",
  fontWeight: "black",
  ml: 2,
  mt: -1,
};

const buttonWrapper = {
  flex: { base: 0, md: 0, sm: 0 },
  justify: "flex-end",
  direction: "row",
  spacing: 6,
};

const loginButton = {
  fontSize: "sm",
  fontWeight: 400,
  variant: "link",
  colorScheme: "purple",
};

const signupButton = {
  display: "inline-flex",
  fontSize: "sm",
  fontWeight: 600,
  colorScheme: "purple",
};

const avatarWrapper = {
  flex: { base: 1, md: 0 },
  justify: "flex-end",
  direction: "row",
  spacing: 6,
};

const styles = {
  wrapper,
  logo,
  logoText,
  buttonWrapper,
  loginButton,
  signupButton,
  avatarWrapper,
};

export default styles;
