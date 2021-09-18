import defaultTheme from "./default";

import { createMuiTheme } from "@material-ui/core";

// eslint-disable-next-line
export default {
  default: createMuiTheme({ ...defaultTheme }),
};
