const { merge, bufferTime, filter } = require("rxjs");
const { TestScheduler } = require("rxjs/testing");

const scheduler = new TestScheduler((actual, expected) => {
  expect(actual).toEqual(expected);
});

it("generates the stream correctly", () => {
  scheduler.run((helpers) => {
    const { cold, hot, time, expectObservable } = helpers;

    const c1 = cold(" -------a----------|");
    const c2 = cold(" -------b----------|");
    const c3 = cold(" --------c---------|");
    const t = time("  --|");
    const expected = "--------x---------|";

    const streams = [c1, c2, c3];

    const observable = merge(...streams)
      .pipe(bufferTime(t))
      .pipe(filter((v) => v.length > 0));

    expectObservable(observable).toBe(expected, {
      x: ["a", "b", "c"],
    });
  });
});
